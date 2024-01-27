using App.Consts;
using App.State;
using Plugin.Maui.Audio;

namespace App.ViewModels
{
    public class VoiceDetectorViewModel : BaseViewModel
    {
        private readonly IAudioManager _audioManager;
        private readonly AppState _state;
        private IAudioPlayer? AudioPlayer { get; set; } = null;

        private string microphoneImageSource = MicrophoneConsts.redColorSource;
        private bool canStart = true;
        private bool canStop = false;
        private bool canPause = false;

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
        public bool CanPause
        {
            get { return canPause; }
            set
            {
                if (canPause != value)
                {
                    canPause = value;
                    OnPropertyChanged(nameof(CanPause));

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

        public Command PauseDetecting { get; set; }

        public VoiceDetectorViewModel(IAudioManager audioManager, AppState appState) : base()
        {
            _audioManager = audioManager;
            _state = appState;

            StartDetecting = new Command(SetDetectingState);

            StopDetecting = new Command(SetAfterDetectingState);

            PauseDetecting = new Command(PauseState);
            base.Commands = [StartDetecting, StopDetecting, PauseDetecting];
        }

        private async void SetDetectingState()
        {
            DisplayedInformation = MicrophoneConsts.whileDetectingMessage;
            MicrophoneImageSource = MicrophoneConsts.greenColorSource;
            CanStart = false;
            CanStop = true;
            CanPause = false;
            if (_state.SoundOn)
            {
                if (AudioPlayer != null)
                {
                    AudioPlayer.Play();
                }
                else
                {
                    AudioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("buttonclick.mp3"));
                    AudioPlayer.Play();
                }
            }

        }

        private void SetAfterDetectingState()
        {
            DisplayedInformation = MicrophoneConsts.afterDetectingMessage;
            MicrophoneImageSource = MicrophoneConsts.redColorSource;
            CanStart = true;
            CanStop = false;
            CanPause = true;

            if (_state.SoundOn)
            {
                AudioPlayer!.Play();
            }

        }

        private void PauseState()
        {
            MicrophoneImageSource = MicrophoneConsts.redColorSource;
            CanStart = false;
            CanStop = false;
            CanPause = false;

            if (_state.SoundOn)
            {
                AudioPlayer!.Play();
            }

        }
    }
}
