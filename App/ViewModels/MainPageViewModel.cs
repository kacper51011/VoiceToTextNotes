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
                try
                {
                    if (!IsNavigating)
                    {
                        IsNavigating = true;
                        RefreshCanExecutes();
                        await Shell.Current.GoToAsync("/VoiceDetector");




                    }
                }
                catch (Exception)
                {
                    throw;
                }



            });

            NavigateToNotes = new Command(async () =>
            {
                try
                {
                    if (!IsNavigating)
                    {
                        IsNavigating = true;

                        RefreshCanExecutes();
                        await Shell.Current.GoToAsync("/Notes");


                    }
                }
                catch (Exception)
                {

                    throw;
                }


            });
            NavigateToGuide = new Command(async () =>
            {
                try
                {
                    if (!IsNavigating)
                    {
                        IsNavigating = true;

                        RefreshCanExecutes();

                        await Shell.Current.GoToAsync("/Notes");


                    }
                }
                catch (Exception)
                {

                    throw;
                }


            });

            ExitApp = new Command(() =>
            {
                Application.Current.Quit();
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void RefreshCanExecutes()
        {
            (NavigateToVoiceDetector as Command).ChangeCanExecute();
            (NavigateToNotes as Command).ChangeCanExecute();
            (NavigateToGuide as Command).ChangeCanExecute();

        }
    }
}
