using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http; 

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Feedback.Services
{
  public abstract partial class BaseWebService
  {
    HttpClient CreateNativeHttpClient()
    {
      return new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());
    }
  }
}