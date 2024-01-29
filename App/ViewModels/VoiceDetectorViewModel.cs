using App.Consts;
using App.Models;
using App.Services.Db;
using App.State;
using App.Views;
using CommunityToolkit.Maui.Media;
using CommunityToolkit.Maui.Views;
using Plugin.Maui.Audio;
using System.Globalization;

namespace App.ViewModels
{
    public class VoiceDetectorViewModel : BaseViewModel
    {
        private readonly IAudioManager _audioManager;
        private readonly ISpeechToText _speechToText;
        private readonly AppState _state;
        private readonly LocalDbService _localDbService;
        private IAudioPlayer? AudioPlayer { get; set; } = null;


        private string greenButtonState = MicrophoneConsts.startButtonState;
        private string microphoneImageSource = MicrophoneConsts.redColorSource;

        private bool canStart = true;
        private bool canStop = false;
        private bool canPause = false;

        public List<string> RecordSlices { get; set; } = new List<string>();
        private string currentRecordState = "";

        public string CurrentRecordState
        {
            get { return currentRecordState; }
            set
            {
                if (currentRecordState != value)
                {
                    currentRecordState = value;
                    OnPropertyChanged(nameof(CurrentRecordState));

                }
            }
        }



        public string GreenButtonState
        {
            get { return greenButtonState; }
            set
            {
                if (greenButtonState != value)
                {
                    greenButtonState = value;
                    OnPropertyChanged(nameof(GreenButtonState));

                }
            }
        }
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

        public VoiceDetectorViewModel(IAudioManager audioManager, AppState appState, ISpeechToText speechToText, LocalDbService dbService) : base()
        {
            _localDbService = dbService;
            


            _speechToText = speechToText;
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
            CanPause = true;
            CanStop = true;

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

            _speechToText.RecognitionResultCompleted += OnRecognitionTextCompleted;
            await _speechToText.StartListenAsync(CultureInfo.CurrentCulture);

        }

        void OnRecognitionTextCompleted(object? sender, SpeechToTextRecognitionResultCompletedEventArgs args)
        {
            CurrentRecordState = args.RecognitionResult;

        }


        private async void PauseState()
        {
            MicrophoneImageSource = MicrophoneConsts.orangeColorSource;
            GreenButtonState = MicrophoneConsts.resumeButtonState;
            DisplayedInformation = MicrophoneConsts.whilePausedMessage;

            CanStart = true;
            CanStop = true;
            CanPause = false;


            if (_state.SoundOn)
            {
                AudioPlayer!.Play();
            }

            _speechToText.RecognitionResultCompleted -= OnRecognitionTextCompleted;
            RecordSlices.Add(CurrentRecordState);
            CurrentRecordState = "";

            await _speechToText.StopListenAsync();
        }

        private async void SetAfterDetectingState()
        {
            try
            {
                DisplayedInformation = MicrophoneConsts.beforeDetectingMessage;
                MicrophoneImageSource = MicrophoneConsts.redColorSource;
                GreenButtonState = MicrophoneConsts.startButtonState;
                CanStart = true;
                CanStop = false;
                CanPause = false;

                if (_state.SoundOn)
                {
                    AudioPlayer!.Play();
                }
                _speechToText.RecognitionResultCompleted -= OnRecognitionTextCompleted;
                RecordSlices.Add(CurrentRecordState);
                var concatenatedRecords = string.Join(" ", RecordSlices.ToArray());

                CurrentRecordState = "";
                RecordSlices.Clear();

                await _speechToText.StopListenAsync();
                var result = await Shell.Current.ShowPopupAsync(new CreateNoteFromVoicePopUp(concatenatedRecords));
                if (result is NoteInformation noteResult)
                {
                    await _localDbService.CreateNote(new Note {Name = noteResult.Name, Content= noteResult.Content});
                    await Shell.Current.DisplayAlert("Congratulations!", "Your Note has been created", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Note rejected", "You Rejected your note", "OK");

                }

            }

            catch (Exception)
            {

                throw;
            }



        }
    }
}
