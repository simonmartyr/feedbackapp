using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Foundation;
using UIKit;

namespace Feedback.Services
{
  public abstract partial class BaseWebService
  {
    HttpClient CreateNativeHttpClient()
    {
      return new HttpClient(new NSUrlSessionHandler());
    }
  }
}