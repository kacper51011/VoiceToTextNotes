using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class VoiceDetectorViewModel : BaseViewModel
    {
        public const string redMicrophoneImageSource = "greenmicrophone.svg";
        public const string greenMicropHoneImageSource = "redmicrophone.svg";

        private string imageSource = "redmicrophone.svg";
        private bool isStarted = false;
        private bool isCompleted = false;

        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));

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



        private string displayedInformation;


        public Command StartDetecting { get; set; }
        public Command StopDetecting { get; set; }

        public Command EditSavedText { get; set; }

        public VoiceDetectorViewModel()
        {
            StartDetecting = new Command(async () =>
            {
                ImageSource = greenMicropHoneImageSource;
            });
        }
    }
}
