using MauiApplication = Microsoft.Maui.MauiApplication;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Media.Session;
using Android.OS;
using Android.Service.Notification;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using CommunityToolkit.Mvvm.Messaging;
using NotificationCompress.Messages;
using Message = NotificationCompress.Models.Message;
using Android.App;
using NotificationCompress.Helps;

namespace NotificationCompress.Services
{
    [Service(Name = "com.li.NotificationCompress.Services.NotificationListener", Permission = "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE",Exported =true)]
    [IntentFilter(new[] { "android.service.notification.NotificationListenerService" })]
    public class NotificationListener : NotificationListenerService
    {
        private SystemServices pmService = IPlatformApplication.Current.Services.GetService(typeof(SystemServices)) as SystemServices;
        private RuleAggregator ruleAggregator = RuleAggregator.Instance;

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
            UpdateNotification(sbn);
            base.OnNotificationPosted(sbn);
        }

        public override void OnNotificationRemoved(StatusBarNotification sbn)
        {
            base.OnNotificationRemoved(sbn);
        }

        public void UpdateNotification(StatusBarNotification sbn)
        {
            CheckNotificationInRule(sbn);
        }

        public void CheckNotificationInRule(StatusBarNotification sbn)
        {
            var ruleIds = ruleAggregator.Actions.Where(x => x.RulePkg.Contains(sbn.PackageName))
                .Select(x => (x.Id,x.RuleType));

            Message message = null;

            foreach (var ruleId in ruleIds) 
            {
                switch (ruleId.RuleType)
                {
                    case RuleEnum.Silence:
                        message = MessageHandle(sbn);
                        ruleAggregator.SilenceMessageAggregator.Add(message);
                        break;
                    case RuleEnum.Filter:
                        message = MessageHandle(sbn);
                        ruleAggregator.FilterMessageAggregator.Add(message);
                        break;
                    case RuleEnum.Transmit:
                        message = MessageHandle(sbn);
                        ruleAggregator.TransmitMessageAggregator.Add(message);
                        break;
                    default:
                        break;
                }
            }
            if(message != null)
            {
                CancelNotification(sbn.Key);
                WeakReferenceMessenger.Default.Send(new GetNotification(message));
            }
        }

       public Message MessageHandle(StatusBarNotification sbn)
       {
            var bundle = sbn.Notification.Extras;

            var isMedia = bundle.GetParcelable(NotificationCompat.ExtraMediaSession) is not null;
            var isGroupHeader = (sbn.Notification.Flags & NotificationFlags.GroupSummary) != 0;
            var isOngoing = (sbn.Notification.Flags & NotificationFlags.OngoingEvent) != 0;
            Message message = null;
            if (bundle.ContainsKey(Notification.ExtraTitle) && !isMedia && !isGroupHeader && !isOngoing && !sbn.PackageName.Contains("com.android.systemui"))
            {
                var id = sbn.Id;
                var pkgName = sbn.PackageName;
                var name = pmService.GetApplicationName(pkgName);
                var title = bundle.GetString(Notification.ExtraTitle) ?? "";
                var content = bundle.GetString(Notification.ExtraText) ?? "";
                var pendingIntent = sbn.Notification.ContentIntent;
                message = Message.Build(id,name,pkgName,title,content,pendingIntent);    
            }
            bundle.Dispose();
            return message;
        }

    }
}
