using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace App.ViewModels
{
    public class VoiceDetectorViewModel : BaseViewModel
    {
        private const string redMicrophoneSource = "redmicrophone.svg";
        private const string greenMicrophoneSource = "greenmicrophone.svg";
        private string microphoneImageSource = redMicrophoneSource;



        private bool isStarted = false;
        private bool isStopped = true;
        private bool isCompleted = false;

        public string MicrophoneImageSource
        {
            get { return microphoneImageSource; }
            set
            {
                if (microphoneImageSource != value)
                {
                    microphoneImageSource = value;
                    OnPropertyChanged(nameof(MicrophoneImageSource));

                }
            }
        }
        public bool IsStarted
        {
            get { return isStarted; }
            set
            {
                if (isStarted != value)
                {
                    isStarted = value;
                    OnPropertyChanged(nameof(IsStarted));

                }
            }
        }
        public bool IsStopped
        {
            get { return isStopped; }
            set
            {
                if (isStopped != value)
                {
                    isStopped = value;
                    OnPropertyChanged(nameof(IsStopped));

                }
            }
        }
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));

                }
            }
        }



        private string displayedInformation = "Click Start button to record!";
        public string DisplayedInformation
        {
            get { return displayedInformation; }
            set
            {
                if (displayedInformation != value)
                {
                    displayedInformation = value;
                    OnPropertyChanged(nameof(DisplayedInformation));

                }
            }
        }

        public Command StartDetecting { get; set; }
        public Command StopDetecting { get; set; }

        public Command EditSavedText { get; set; }

        public VoiceDetectorViewModel(): base()
        {

            StartDetecting = new Command(async () =>
            {
                DisplayedInformation = "Stop recording";
                MicrophoneImageSource = greenMicrophoneSource;
            });

            StopDetecting = new Command(async () =>
            {
                
            });

            EditSavedText = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });
            base.Commands = [StartDetecting, StopDetecting, EditSavedText];
        }
    }
}
