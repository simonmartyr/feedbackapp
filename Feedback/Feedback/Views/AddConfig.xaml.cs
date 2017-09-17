using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Feedback
{
  public partial class AddConfig : ContentPage
  {
    public AddConfig()
    {
      InitializeComponent();
      AddConfigViewModel vm = new AddConfigViewModel();
      vm.Navigation = Navigation;
      BindingContext = vm;
    }
  }
}
