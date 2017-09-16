using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using System.Text;
using System.Net.Http;
using System.Xml; 

namespace Feedback.Services.Implementation
{
  class FeedbackAPI : BaseWebService
  {
    static string URL = "";

    public FeedbackAPI()
    {
      Init(URL);
    }

    public async Task<string> PostXMLAsync(string requestUri, string APIKey, string AppId)
    {
      var soapString = Resources.Constants.ConstructSoapRequest(APIKey, AppId);
      //var jsonRequest = !obj.Equals(default(K)) ? JsonConvert.SerializeObject(obj) : null;
      var request = new StringContent(soapString, Encoding.UTF8, "text/xml");
      var response = await PostAsync(requestUri, request);
      string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false) ?? string.Empty;

      return json;
    }


  }
}
