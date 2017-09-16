using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feedback.Models; 
using Feedback.Services.Implementation; 
using Xamarin.Forms;
using System.Xml;

namespace Feedback
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

    protected async override void OnAppearing()
    {
      FeedbackAPI Feedback = new FeedbackAPI();
      SprinterConfigs conf = new SprinterConfigs();
      IList < SprinterConfigs > configs = new List<SprinterConfigs>();
      configs.Add(conf);
      await Feedback.RequestFeedback(configs);
      //var result = await Feedback.PostXMLAsync("3ab1eb8b-fdff-4c18-a73c-313d9e7843fd", "65d6a1b9-edee-4048-9c6c-b079ae393374");
      //Console.WriteLine(result);
      //XmlDocument xmlDoc = new XmlDocument();
      //xmlDoc.LoadXml(result);
      //var issues = xmlDoc.GetElementsByTagName("Issues");

      base.OnAppearing();
    }
  }
}
