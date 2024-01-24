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

        private Command NavigateToVoiceDetector {  get; set; }
        private Command NavigateToNotes {  get; set; }
        private Command ExitApp {  get; set; }

        public MainPageViewModel()
        {
            NavigateToVoiceDetector = new Command(async () =>
            {
                await Shell.Current.GoToAsync("/VoiceDetector");
            });

            NavigateToNotes = new Command(async () =>
            {
                await Shell.Current.GoToAsync("/Notes");
            });

        }

    }
}
