using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected Command[] Commands { get; set; } = [];

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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void ResetCommands()
        {
            foreach (var command in Commands)
            {
                command.ChangeCanExecute();
            }

        }

        protected async void NavigateFromMainPageTo(string url)
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
        protected bool CanExecuteNavigation(string url)
        {
            return !IsNavigating;
        }

    }
}
