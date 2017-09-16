using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.Services.Resources
{
  class Constants
  {


    public static string ConstructSoapRequest(string APIKey, string AppID)
    {
      return String.Format(@"<s11:Envelope xmlns:s11='http://schemas.xmlsoap.org/soap/envelope/'>
        <s11:Header>
          <ns1:authentication xmlns:ns1='http://home.mendix.com/feedback'>
            <username>PlatformAPIUser</username>
            <password>PlatformAPIPassword</password>
          </ns1:authentication>
        </s11:Header>
        <s11:Body>
          <ns1:GetFeedbackItems xmlns:ns1='http://home.mendix.com/feedback'>
            <ApiKey>{0}</ApiKey>
            <ProjectID>{1}</ProjectID>
          </ns1:GetFeedbackItems>
        </s11:Body>
        </s11:Envelope>", APIKey, AppID);
    }

  }
}
