using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NotificationCompress.Pages;
using NotificationCompress.Services;
using NotificationCompress.ViewModels;

namespace NotificationCompress
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
            builder.Services
                .AddSingleton<SystemServices>()
                .AddSingleton<MainPageViewModel>()
                .AddSingleton<AppFiltePageViewModel>()
                .AddSingleton<MainPage>()
                .AddSingleton<AppFiltePage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
