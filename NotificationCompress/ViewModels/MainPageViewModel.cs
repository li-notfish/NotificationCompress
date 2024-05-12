using Android.Content;
using Android.Graphics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NotificationCompress.Helps;
using NotificationCompress.Messages;
using NotificationCompress.Services;
using System.Collections.ObjectModel;
using AndroidApplication = Android.App.Application;
using Message = NotificationCompress.Models.Message;

namespace NotificationCompress.ViewModels
{
    public partial class MainPageViewModel :ObservableRecipient
    {
        private readonly SystemServices pmServices;

        private readonly IconCache iconCache = IconCache.Instance;

        [ObservableProperty]
        private bool isEnableListen = false;

        [ObservableProperty]
        private ObservableCollection<Message> messages = new ObservableCollection<Message>();

        public MainPageViewModel(SystemServices systemServices)
        {
            pmServices = systemServices;

            WeakReferenceMessenger.Default.Register<GetNotification>(this, (r, m) =>
            {
                Bitmap res = null;
                iconCache.BitmapCache.TryGetValue(m.Value.PackageName, out res);
                if (res == null)
                {
                    res = pmServices.GetRawIconBitmap(m.Value.PackageName);
                    iconCache.BitmapCache.Add(m.Value.PackageName, res);
                }
                var buff = BitImageHelp.GetImageStream(res);
                m.Value.IconBytes = buff;
                Messages.Add(m.Value);
            });
            InitProcess();
        }

        public async void InitProcess()
        {
            IsEnableListen = NotificationPermission.CheckNotificationAccess();
            var status = PermissionStatus.Unknown;

            status = await Permissions.CheckStatusAsync<NotificationPermission>();

            if (status == PermissionStatus.Granted && IsEnableListen)
            {
                return;
            }

            if (Permissions.ShouldShowRationale<NotificationPermission>())
            {
                await Shell.Current.DisplayAlert("需要权限", "需要进行授权通知监听才能继续！", "好捏");
            }

            status = await Permissions.RequestAsync<NotificationPermission>();

            if (status == PermissionStatus.Denied)
            {
                await Shell.Current.DisplayAlert("需要权限", "需要赋予监听通知权限才能继续，否则无法进行设置", "ok");
            }
        }

        [RelayCommand]
        public void EnableListen()
        {
            if (!NotificationPermission.CheckNotificationAccess())
            {
                Intent intent = new Intent("android.settings.ACTION_NOTIFICATION_LISTENER_SETTINGS");
                intent.SetFlags(ActivityFlags.NewTask);
                AndroidApplication.Context.StartActivity(intent);
            }
        }

        [RelayCommand]
        public void ClearsNotifications()
        {
            Messages.Clear();
        }
    }
}
