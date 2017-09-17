using System;
using Xamarin.Forms;
using Realms;
using Feedback.Models;
using System.Windows.Input;

namespace Feedback
{
  public class AddConfigViewModel
  {
    public INavigation Navigation { get; set; }
    public string AppName { get; set; }
    public string ApiKey { get; set; }
    public string AppID { get; set; }
    public ICommand SaveButton { get; private set; }

    readonly Realm realm;

    public AddConfigViewModel()
    {
      realm = Realm.GetInstance();
      SaveButton = new Command(SaveConfig);
    }

    void SaveConfig()
    {
      realm.Write(() =>
      {
        var toSave = new SprinterConfigs() { AppName = AppName, ApiKey = ApiKey, AppId = AppID };
        realm.Add(toSave);
      });

      Navigation.PopAsync();
    }

  }
}
