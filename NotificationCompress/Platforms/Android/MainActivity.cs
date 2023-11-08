using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using NotificationCompress.Helps;

namespace NotificationCompress
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true,Exported =true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(new[] { Intent.ActionMain },Categories = new[] {Intent.CategoryLauncher})]
    public class MainActivity : MauiAppCompatActivity
    {
        
    }
}
