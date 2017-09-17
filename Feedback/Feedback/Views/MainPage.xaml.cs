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
      MainPageViewModel vm = new MainPageViewModel();
      vm.Navigation = Navigation; 
      BindingContext = vm;

		}

    protected async override void OnAppearing()
    {
      /*
      FeedbackAPI Feedback = new FeedbackAPI();
      SprinterConfigs conf = new SprinterConfigs();
      IList < SprinterConfigs > configs = new List<SprinterConfigs>();
      configs.Add(conf);
      await Feedback.RequestFeedback(configs);

      base.OnAppearing();
      */
    }
  }
}
