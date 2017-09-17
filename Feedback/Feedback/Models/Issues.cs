using System;
using System.Xml.Serialization;
using Realms;
namespace Feedback.Models
{
  [XmlRoot(ElementName = "Issues")]
  public class Issues : RealmObject
  {
    [PrimaryKey]
    public int Nr { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string IssueState { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserRoles { get; set; }
    public string Browser { get; set; }
    //[XmlElement("Timestamp")]
    //public DateTimeOffset TimeStamp { get; set; }
    public string Form { get; set; }
    public string FormGuide { get; set; }
    public string Url { get; set; }
    public string ScreenSize { get; set; }
    public string IssueType { get; set; }

  }
}


/*<Nr>289479</Nr>
<Shortname><![CDATA[risk submitted without expiration date]]></Shortname>
<Description><![CDATA[Risk - PrimeSouth in Southern Trust 2017-18 program]]></Description>
<IssueState><![CDATA[Scheduled]]></IssueState>
<Username><![CDATA[kcaruso]]></Username>
<Useremail><![CDATA[kmassirio@archrefac.com]]></Useremail>
<Userroles><![CDATA[ARFUnderwriter ARFAdministrator ARFPriceApprover ARFAssistantUnderwriter (account: kcaruso)]]></Userroles>
<Browser><![CDATA[Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3; rv:11.0) like Gecko]]></Browser>
<Timestamp>2017-08-29T14:54:27.620Z</Timestamp>
<Form><![CDATA[]]></Form>
<FormGuid xsi:nil="true" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"></FormGuid>
<Url><![CDATA[https://clients.archrefac.com/index3.html#1504018263514-25]]></Url>
<Screensize><![CDATA[1280x1024]]></Screensize>
<IssueType><![CDATA[Problem]]></IssueType>
*/
