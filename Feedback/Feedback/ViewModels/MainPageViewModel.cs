using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Feedback.Models;
using Realms;
using Xamarin.Forms;

namespace Feedback
{
  public class MainPageViewModel 
  {
    public INavigation Navigation { get; set; }
    public IRealmCollection<SprinterConfigs> ConfigsList { get; set; }
    public ICommand ButtonCommand { get; private set; }
    public String Title { get; set; }

    public MainPageViewModel()
    {
      var realm = Realm.GetInstance(); 
      ConfigsList = (Realms.IRealmCollection<Feedback.Models.SprinterConfigs>)realm.All<SprinterConfigs>();
      ButtonCommand = new Command(AddNew);
      Title = "Sprinter Configs"; 
    }


    void AddNew(){
      Navigation.PushAsync(new AddConfig());
    }
   
  }
}
