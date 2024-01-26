using App.Models;
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



        private bool canStart = true;
        private bool canStop = false;
        private bool canEdit = false;

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
        public bool CanStart
        {
            get { return canStart; }
            set
            {
                if (canStart != value)
                {
                    canStart = value;
                    OnPropertyChanged(nameof(CanStart));

                }
            }
        }
        public bool CanStop
        {
            get { return canStop; }
            set
            {
                if (canStop != value)
                {
                    canStop = value;
                    OnPropertyChanged(nameof(CanStop));

                }
            }
        }
        public bool CanEdit
        {
            get { return canEdit; }
            set
            {
                if (canEdit != value)
                {
                    canEdit = value;
                    OnPropertyChanged(nameof(CanEdit));

                }
            }
        }



        private string displayedInformation = MicrophoneConsts.beforeDetectingMessage;
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

        public Command StartEditing { get; set; }

        public VoiceDetectorViewModel(): base()
        {

            StartDetecting = new Command(SetDetectingState);

            StopDetecting = new Command(SetAfterDetectingState);

            StartEditing = new Command(SetWhileEditingState);
            base.Commands = [StartDetecting, StopDetecting, StartEditing];
        }

        private void SetDetectingState()
        {
            DisplayedInformation = MicrophoneConsts.whileDetectingMessage;
            MicrophoneImageSource = MicrophoneConsts.greenColorSource;
            CanStart = false;
            CanStop = true;
            CanEdit = false;
        }

        private void SetAfterDetectingState()
        {
            DisplayedInformation = MicrophoneConsts.afterDetectingMessage;
            MicrophoneImageSource = MicrophoneConsts.redColorSource;
            CanStart = true;
            CanStop = false;
            CanEdit = true;
        }

        private void SetWhileEditingState()
        {
            MicrophoneImageSource = MicrophoneConsts.redColorSource;
            CanStart = false;
            CanStop = false;
            CanEdit = false;
        }
    }
}
