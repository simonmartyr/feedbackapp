using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using System.Text;
using System.Net.Http;
using System.Xml;
using Feedback.Models;
using System.Xml.Serialization;
using System.IO;

namespace Feedback.Services.Implementation
{
  class FeedbackAPI : BaseWebService
  {
    static string URL = "https://sprintr.home.mendix.com/ws/FeedbackAPI";

    public FeedbackAPI()
    {
      Init(URL);
    }

    public async Task RequestFeedback(IList<SprinterConfigs> configs){
      foreach (SprinterConfigs i in configs)
      {
        var raw = await PostXMLAsync(i.ApiKey, i.AppId);
        if (i.RawCache == raw) //matches our cache
          return;

        i.RawCache = raw; //update cache
        ProcessXML(raw);
      }
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

    private void ProcessXML(string XML){
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(XML);
      var issues = doc.GetElementsByTagName("Issues");
      foreach(XmlNode i in issues){
        //Issues issue = new Issues();
       

        string json = JsonConvert.SerializeXmlNode(i);
        var result = DeserializeJSON<Issues>(json);
        Console.WriteLine(result.Nr);
      }
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
