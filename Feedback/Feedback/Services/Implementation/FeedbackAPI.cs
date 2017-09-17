using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Xml;
using Feedback.Models;
using System.Xml.Serialization;
using Realms;
using System.IO;

namespace Feedback.Services.Implementation
{
  class FeedbackAPI : BaseWebService
  {
    static string URL = "https://sprintr.home.mendix.com/ws/FeedbackAPI";
    Realm realm;

    public FeedbackAPI()
    {
      Init(URL);
      realm = Realm.GetInstance();
    }

    public async Task RequestFeedback(IList<SprinterConfigs> configs)
    {
      foreach (SprinterConfigs i in configs)
      {
        var raw = await PostXMLAsync(i.ApiKey, i.AppId);
        if (i.RawCache == raw) //matches our cache
          return;

        i.RawCache = raw; //update cache
        ProcessXML(raw, i);
      }
    }

    public async Task ProcessFeedback(SprinterConfigs config)
    {
      var raw = await PostXMLAsync(config.ApiKey, config.AppId);
      if (config.RawCache == raw) //matches our cache
        return;

      //config.RawCache = raw; //update cache
      ProcessXML(raw, config);


    }

    private async Task<string> PostXMLAsync(string APIKey, string AppId)
    {
      if (!await CanExecute())
        return null;

      var soapString = Resources.Constants.ConstructSoapRequest(APIKey, AppId);
      var request = new StringContent(soapString, Encoding.UTF8, "text/xml");
      var response = await PostAsync("", request);
      string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false) ?? string.Empty;

      return json;
    }

    private void ProcessXML(string XML, SprinterConfigs config)
    {
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(XML);
      var issues = doc.GetElementsByTagName("Issues");

      SprinterConfigs ToUpdate = new SprinterConfigs
      {
        ApiKey = config.ApiKey,
        AppId = config.AppId,
        AppName = config.AppName,
        RawCache = XML
      };

      foreach (XmlNode i in issues)
      {
        var result = ConvertNode<IssuesImporter>(i);
        ToUpdate.Issues.Add(new Issues
        {
          Browser = result.Browser,
          Description = result.Description,
          Form = result.Form,
          FormGuide = result.FormGuide,
          IssueState = result.IssueState,
          Nr = result.Nr,
          IssueType = result.IssueType,
          ScreenSize = result.ScreenSize,
          ShortName = result.ShortName,
          Url = result.Url,
          UserEmail = result.UserEmail,
          UserName = result.UserName,
          UserRoles = result.UserRoles
        });
      }
      realm.Write(() => realm.Add(ToUpdate, update: true));
    }

    private static T ConvertNode<T>(XmlNode node) where T : class
    {
      MemoryStream stm = new MemoryStream();

      StreamWriter stw = new StreamWriter(stm);
      stw.Write(node.OuterXml);
      stw.Flush();

      stm.Position = 0;

      XmlSerializer ser = new XmlSerializer(typeof(T));
      T result = (ser.Deserialize(stm) as T);

      return result;
    }

  }
}
