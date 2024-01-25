using App.ViewModels;
using App.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

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

            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<VoiceDetectorView>();
            builder.Services.AddTransient<VoiceDetectorViewModel>();

            builder.Services.AddTransient<NotesViewModel>();
            builder.Services.AddTransient<NotesView>();

            builder.Services.AddTransient<GuideViewModel>();
            builder.Services.AddTransient<GuideView>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
