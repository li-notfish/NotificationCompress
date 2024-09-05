using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NotificationCompress.Helps;
using NotificationCompress.Pages;
using NotificationCompress.Services;
using NotificationCompress.View.Popups;
using NotificationCompress.ViewModels;
using NotificationCompress.ViewModels.Popups;

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
                .AddSingleton<ActionsPageViewModel>()
                .AddTransient<MainPage>()
                .AddTransient<ActionsPage>()
                .AddSingleton<LocalDatabase>()
                .AddSingleton(IconCache.Instance)
                .AddSingleton(RuleAggregator.Instance)
                .AddTransientPopup<AppFilterPage, AppFilterPageViewModel>()
                .AddTransientPopup<ShowFilterMessagePopup,ShowFilterMessagePopupViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
