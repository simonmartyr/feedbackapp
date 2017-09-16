using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace Feedback.Services
{
  public abstract partial class BaseWebService
  {
    //Native Message Handler handled on each device. 

    protected enum RequestType { Delete, Get, Post, Put }
    static HttpClient client;
    string BaseServiceUrl { set; get; }

    protected void Init(string baseURI, Action<HttpClient> clientModifer = null)
    {
      client = CreateNativeHttpClient();
      this.BaseServiceUrl = baseURI;
      client.BaseAddress = new Uri(BaseServiceUrl);

      if (clientModifer != null)
      {
        clientModifer(client);
      }
    }

    public async Task<bool> CanExecute()
    {
      if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
      {
        var host = new Uri(BaseServiceUrl).Host;
        var serviceIsReachable = await Plugin.Connectivity.CrossConnectivity.Current.IsRemoteReachable(host, msTimeout: 2000);
        if (serviceIsReachable)
        {
          return true;
        }
      }
      return false;
    }

    protected T DeserializeJSON<T>(string json) //Convert retruned json into an obj 
    {
      if (string.IsNullOrEmpty(json)) return default(T);
      var token = JToken.Parse(json);

      var obj = token.ToObject<T>();
      return obj;
    }

    protected T DeserializeSOAP<T>(string xml)
    {
      if (string.IsNullOrEmpty(xml)) return default(T);
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(xml);
      return DeserializeJSON<T>(JsonConvert.SerializeObject(doc)); 
    }

    protected Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
      return SendAsync(RequestType.Delete, requestUri);
    }

    protected Task<HttpResponseMessage> GetAsync(string requestUri)
    {
      return SendAsync(RequestType.Get, requestUri);
    }

    protected Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
    {
      return SendAsync(RequestType.Post, requestUri, content);
    }

    protected Task<HttpResponseMessage> PostAsync(string requestUri)
    {
      return SendAsync(RequestType.Post, requestUri);
    }


    protected Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
    {
      return SendAsync(RequestType.Put, requestUri, content);
    }

    protected Task<HttpResponseMessage> PutAsync(string requestUri)
    {
      return SendAsync(RequestType.Put, requestUri);
    }

    public void clearHeaders()
    {
      client.DefaultRequestHeaders.Clear();
    }

    public void addHeader(string key, string value)
    {
      client.DefaultRequestHeaders.Add(key, value);
    }

    protected async Task<HttpResponseMessage> SendAsync(RequestType requestType, string requestUri, HttpContent content = null)
    {
      Task<HttpResponseMessage> httpTask;

      switch (requestType)
      {
        case RequestType.Delete:
          httpTask = client.DeleteAsync(requestUri);
          break;
        case RequestType.Get:
          httpTask = client.GetAsync(requestUri);
          break;
        case RequestType.Post:
          httpTask = client.PostAsync(requestUri, content);
          break;
        case RequestType.Put:
          httpTask = client.PutAsync(requestUri, content);
          break;
        default:
          throw new Exception("not valid request");
      }

      var response = await httpTask.ConfigureAwait(false);

      if (response.IsSuccessStatusCode)
        return response;

      IEnumerable<string> values;
      string msg = string.Empty;
      if (response.Headers.TryGetValues("msg", out values))
        msg = values.FirstOrDefault();

      throw new Exception("WS failed - " + msg);
    }

  }

}
