
using System.ComponentModel;

namespace App.ViewModels
{
    public class MainPageViewModel : BaseViewModel, INotifyPropertyChanged
    {

        public Command NavigateToCommand { get; set; }
        public Command ExitApp { get; set; }

        public MainPageViewModel(): base()
        {
 
            NavigateToCommand = new Command<string>(NavigateFromMainPageTo, CanExecuteNavigation);
           base.Commands = [NavigateToCommand];

            ExitApp = new Command(Application.Current.Quit);


        }

        

    }
}
