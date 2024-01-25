using App.Helpers;
using System.ComponentModel;

namespace App.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private bool isNavigating;

        public bool IsNavigating
        {
            get { return isNavigating; }
            set
            {
                if (isNavigating != value)
                {
                    isNavigating = value;
                    OnPropertyChanged(nameof(IsNavigating));

                }
            }
        }

        public Command NavigateToVoiceDetector { get; set; }
        public Command NavigateToNotes { get; set; }
        public Command NavigateToGuide { get; set; }
        public Command ExitApp { get; set; }

        public MainPageViewModel()
        {

            NavigateToVoiceDetector = new Command(async () =>
            {
                IsNavigating = false;
                await NavigationHelper.NavigateAndRefreshCommands("/VoiceDetector", NavigateToVoiceDetector!, NavigateToNotes!, NavigateToGuide!);
            });

            NavigateToNotes = new Command(async () =>
            {
                await NavigationHelper.NavigateAndRefreshCommands("/Notes", NavigateToVoiceDetector!, NavigateToNotes!, NavigateToGuide!);
            });
            NavigateToGuide = new Command(async () =>
            {
                await NavigationHelper.NavigateAndRefreshCommands("/Guide", NavigateToVoiceDetector!, NavigateToNotes!, NavigateToGuide!);
            });

            ExitApp = new Command(Application.Current.Quit);



        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
