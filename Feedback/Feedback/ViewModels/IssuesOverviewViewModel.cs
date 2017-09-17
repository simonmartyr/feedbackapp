using System;
using Feedback.Models;
using Xamarin.Forms;
using Feedback.Services.Implementation;
using Realms;
using System.Windows.Input;
using System.ComponentModel;

namespace Feedback
{
  public class IssuesOverviewViewModel : INotifyPropertyChanged
  {
    private FeedbackAPI Feedback = new FeedbackAPI();
    public IRealmCollection<Issues> IssuesList { get; set; }
    public ICommand LoadIssues { get; private set; }
    public string Name { get; set; }
    SprinterConfigs Focus;
    Realm realm;
    public IssuesOverviewViewModel(SprinterConfigs Config)
    {
      Focus = Config;
      realm = Realm.GetInstance();
      LoadIssues = new Command(RefreshIssues);
      IssuesList = (Realms.IRealmCollection<Issues>)Config.Issues;
      Name = Focus.AppName;
    }

    async void RefreshIssues()
    {
      if (IsBusy)
        return;

      IsBusy = true;

      await Feedback.ProcessFeedback(Focus);

      IsBusy = false;
    }


    private bool isBusy;

    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged(String info)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(info));
      }
    }

    public bool IsBusy
    {
      get { return isBusy; }
      set
      {
        if (isBusy == value)
          return;

        isBusy = value;
        NotifyPropertyChanged("IsBusy");
      }
    }
  }
}
