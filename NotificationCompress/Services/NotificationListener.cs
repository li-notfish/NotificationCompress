using Android.App;
using Android.Content;
using Android.OS;
using Android.Service.Notification;

namespace NotificationCompress.Services
{
    [Service(Name = "com.li.NotificationCompress.Services.NotificationListener", Permission = "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE",Exported =true)]
    [IntentFilter(new[] { "android.service.notification.NotificationListenerService" })]
    public class NotificationListener : NotificationListenerService
    {
        public override void OnCreate()
        {
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

            base.OnNotificationPosted(sbn);
        }

        public override void OnNotificationRemoved(StatusBarNotification sbn)
        {
            base.OnNotificationRemoved(sbn);
        }

        public void UpdateNotification(StatusBarNotification sbn)
        {

        }
    }
}
