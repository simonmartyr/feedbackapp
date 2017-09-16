using System;
using System.Collections.Generic;
using Realms; 

namespace Feedback.Models
{
  public class SprinterConfigs : RealmObject
  {
    public string AppName {get;set;}
    [PrimaryKey]
    public string ApiKey { get; set; }
    public string AppId { get; set; }
    public string RawCache { get; set; }
    public IList<Issues> Issues { get; }
  }
}
