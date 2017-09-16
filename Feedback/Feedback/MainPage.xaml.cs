using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feedback.Services.Implementation; 
using Xamarin.Forms;

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
      var result = await Feedback.PostXMLAsync("https://sprintr.home.mendix.com/ws/FeedbackAPI", "", "");
      Console.WriteLine(result);
      base.OnAppearing();
    }
  }
}
