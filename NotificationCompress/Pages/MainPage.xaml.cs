using Android.App;
using Android.Content;
using NotificationCompress.Helps;
using NotificationCompress.ViewModels;
using Application = Android.App.Application;

namespace NotificationCompress.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if(e.Value == true)
            {
                var status = PermissionStatus.Unknown;

                status = await Permissions.CheckStatusAsync<NotificationPermission>();

                var accessNotification = NotificationPermission.CheckNotificationAccess();

                if (accessNotification != true)
                {
                    Intent intent = new Intent("android.settings.ACTION_NOTIFICATION_LISTENER_SETTINGS");
                    intent.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(intent);
                }

                if (status == PermissionStatus.Granted && accessNotification)
                {
                    return;
                }

                if (Permissions.ShouldShowRationale<NotificationPermission>())
                {
                    await Shell.Current.DisplayAlert("需要权限", "需要进行授权通知监听才能继续！","好捏");
                }
                Console.WriteLine(status);

                status = await Permissions.RequestAsync<NotificationPermission>();
                Console.WriteLine(status);

                if (status == PermissionStatus.Denied)
                {
                    await Shell.Current.DisplayAlert("需要权限","需要赋予监听通知权限才能继续，否则无法进行设置","ok");
                    ListenEnable.IsToggled = accessNotification;
                }
            }
        }
    }

}
