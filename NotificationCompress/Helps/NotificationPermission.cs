using Android.Provider;
using AndroidX.Core.App;
using NotificationCompress.Services;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Application = Android.App.Application;

namespace NotificationCompress.Helps
{
    public class NotificationPermission : BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new List<(string permission, bool isRuntime)>
        {
            ("android.permission.POST_NOTIFICATIONS",true),
        }.ToArray();

        public static bool CheckNotificationAccess()
        {
            var contentResolver = Application.Context.ContentResolver;
            var enableNotificationListeners = Settings.Secure.GetString(contentResolver, "enabled_notification_listeners");
            var packageName = Application.Context.PackageName;
            return NotificationManagerCompat.GetEnabledListenerPackages(Application.Context).Contains(packageName) &&
                (enableNotificationListeners != null) &&
                enableNotificationListeners.Contains(nameof(NotificationListener));
        }
    }
}
