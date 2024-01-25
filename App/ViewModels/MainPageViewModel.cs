using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.ViewModels
{
    public class MainPageViewModel
    {
        private bool isNavigating { get; set; } = false;

        public Command NavigateToVoiceDetector {  get; set; }
        public Command NavigateToNotes { get; set; }
        public Command ExitApp { get; set; }

        public MainPageViewModel()
        {
            isNavigating = false;

            NavigateToVoiceDetector = new Command(async () =>
            {
                isNavigating = true;
                await Shell.Current.GoToAsync("/VoiceDetector");
            }, canExecute: () =>
            {
                return !isNavigating;
            });

            NavigateToNotes = new Command(async () =>
            {
                isNavigating = true;
                await Shell.Current.GoToAsync("/Notes");
            }, canExecute: () =>
            {
                return !isNavigating;
            });

            ExitApp = new Command( () =>
            {
                Application.Current.Quit();
            });
        }

    }
}
