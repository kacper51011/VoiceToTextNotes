
using System.ComponentModel;

namespace App.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private bool isNavigating = false;

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

        public Command NavigateToCommand { get; set; }
        public Command ExitApp { get; set; }

        public MainPageViewModel()
        {
            NavigateToCommand = new Command<string>(NavigateFromMainPageTo, CanExecuteNavigation);


            ExitApp = new Command(Application.Current.Quit);



        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void ResetCommands()
        {
            (NavigateToCommand as Command).ChangeCanExecute();

        }

        async void NavigateFromMainPageTo(string url)
        {
            try
            {
                IsNavigating = true;
                ResetCommands();
                await Shell.Current.GoToAsync(url);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void NavigatedToMainPage()
        {
            IsNavigating = false;
            ResetCommands();
        }
        private bool CanExecuteNavigation(string url)
        {
            return !IsNavigating;
        }

        

    }
}
