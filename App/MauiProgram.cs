using App.Services.Db;
using App.State;
using App.ViewModels;
using App.Views;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Media;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<ISpeechToText>(SpeechToText.Default);

            builder.Services.AddSingleton<LocalDbService>();
            builder.Services.AddSingleton<AppState>();

            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<VoiceDetectorView>();
            builder.Services.AddTransient<VoiceDetectorViewModel>();

            builder.Services.AddTransient<NotesViewModel>();
            builder.Services.AddTransient<NotesView>();

            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<SettingsView>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
