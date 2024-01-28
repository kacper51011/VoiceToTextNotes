
using Android.Content.PM;
using CommunityToolkit.Maui.Media;
using System.ComponentModel;

namespace App.ViewModels
{
    public class MainPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        ISpeechToText _speechToText;

        public Command NavigateToCommand { get; set; }
        public Command NavigateToVoiceDetector {  get; set; }
        public Command ExitApp { get; set; }

        public MainPageViewModel(ISpeechToText speechToText): base()
        {
            _speechToText = speechToText;
 
            NavigateToCommand = new Command<string>(NavigateFromPageTo, CanExecuteNavigation);
            NavigateToVoiceDetector = new Command<string>(async (url) =>
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Microphone>();

                    if (status != PermissionStatus.Granted)
                    {
                        await Shell.Current.DisplayAlert("Microphone Permission", "Microphone permission is neccessary for application to work properly", "OK");
                        return;
                    }
                    NavigateFromPageTo(url);
                    return;

                }
                NavigateFromPageTo(url);
            }, CanExecuteNavigation);
           base.Commands = [NavigateToCommand, NavigateToVoiceDetector];

            ExitApp = new Command(Application.Current.Quit);


        }

        

    }
}
