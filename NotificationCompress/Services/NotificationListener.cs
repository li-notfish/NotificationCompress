using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Service.Notification;
using CommunityToolkit.Mvvm.Messaging;
using NotificationCompress.Messages;
using Message = NotificationCompress.Models.Message;

namespace NotificationCompress.Services
{
    [Service(Name = "com.li.NotificationCompress.Services.NotificationListener", Permission = "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE",Exported =true)]
    [IntentFilter(new[] { "android.service.notification.NotificationListenerService" })]
    public class NotificationListener : NotificationListenerService
    {
        private PackageManager PackageManager;

        public override void OnCreate()
        {
            PackageManager = Application.PackageManager;
            base.OnCreate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            return base.OnBind(intent);
        }

        public override bool OnUnbind(Intent intent)
        {
            return base.OnUnbind(intent);
        }

        public override void OnRebind(Intent intent)
        {
            base.OnRebind(intent);
        }

        public override void OnNotificationPosted(StatusBarNotification sbn)
        {
            UpdateNotification(sbn);
            base.OnNotificationPosted(sbn);
        }

        public override void OnNotificationRemoved(StatusBarNotification sbn)
        {
            base.OnNotificationRemoved(sbn);
        }

        public void UpdateNotification(StatusBarNotification sbn)
        {
            var bundle = sbn.Notification.Extras;
            var isMedia = bundle.GetParcelable(Notification.ExtraMediaSession) is not null;
            var isGroupHeader = (sbn.Notification.Flags & NotificationFlags.GroupSummary) != 0;
            var isOngoing = (sbn.Notification.Flags & NotificationFlags.OngoingEvent) != 0;
            Message message = new Message();
            if(bundle.ContainsKey(Notification.ExtraTitle) && !isMedia &&!isGroupHeader && !isOngoing && !sbn.PackageName.Contains("com.android.systemui"))
            {
                message.Name = PackageManager.GetApplicationLabel(PackageManager.GetApplicationInfo(sbn.PackageName, 0));
                message.PackageName = sbn.PackageName;
                message.Id = sbn.Id;
                message.Title = bundle.GetString(Notification.ExtraTitle) ?? "";
                message.Content = bundle.GetString(Notification.ExtraText) ?? "";
                message.PendingIntent = sbn.Notification.ContentIntent;
                try
                {
                    if(Build.VERSION.SdkInt >= BuildVersionCodes.P)
                    {
                        message.Icon = sbn.Notification.SmallIcon.ResId;
                    }
                    else
                    {
                        message.Icon = sbn.Notification.Icon;
                    }
                }
                catch (Exception ex)
                {
                    message.Icon = 0;
                }
                WeakReferenceMessenger.Default.Send(new GetNotification(message));
                bundle.Dispose();
            }

        }
    }
}
