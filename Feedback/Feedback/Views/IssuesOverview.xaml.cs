using System;
using System.Collections.Generic;
using Feedback.Models;

using Xamarin.Forms;

namespace Feedback
{
  public partial class IssuesOverview : ContentPage
  {
    public IssuesOverview(SprinterConfigs config)
    {
      InitializeComponent();
      var ViewModel = new IssuesOverviewViewModel(config);
      BindingContext = ViewModel;
    }
  }
}
