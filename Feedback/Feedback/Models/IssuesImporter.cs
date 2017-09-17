using System;
using System.Xml.Serialization;
namespace Feedback.Models
{
  [XmlRoot(ElementName = "Issues")]
  public class IssuesImporter
  {

    [XmlElement("Nr")]
    public int Nr { get; set; }
    [XmlElement("Shortname")]
    public string ShortName { get; set; }
    [XmlElement("Description")]
    public string Description { get; set; }
    [XmlElement("IssueState")]
    public string IssueState { get; set; }
    [XmlElement("Username")]
    public string UserName { get; set; }
    [XmlElement("Useremail")]
    public string UserEmail { get; set; }
    [XmlElement("Userroles")]
    public string UserRoles { get; set; }
    [XmlElement("Browser")]
    public string Browser { get; set; }
    //[XmlElement("Timestamp")]
    //public DateTimeOffset TimeStamp { get; set; }
    [XmlElement("Form")]
    public string Form { get; set; }
    [XmlElement("FormGuid")]
    public string FormGuide { get; set; }
    [XmlElement("Url")]
    public string Url { get; set; }
    [XmlElement("Screensize")]
    public string ScreenSize { get; set; }
    [XmlElement("IssueType")]
    public string IssueType { get; set; }
  }
}

