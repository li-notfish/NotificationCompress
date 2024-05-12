using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using AndroidApplication = Android.App.Application;


namespace NotificationCompress.Services
{
    public class SystemServices
    {
        public PackageManager packageManager = new Lazy<PackageManager>(() => AndroidApplication.Context.PackageManager).Value;

        public Bitmap GetRawIconBitmap(string packageName)
        {
            Bitmap res = null;

            try
            {
                Drawable icon = GetApplicationInfo(packageName).LoadIcon(packageManager);
                res = Bitmap.CreateBitmap(icon.IntrinsicWidth, icon.IntrinsicHeight, Bitmap.Config.Argb8888);
                using (var canvas = new Canvas(res))
                {
                    icon.SetBounds(0, 0, canvas.Width, canvas.Height);
                    icon.Draw(canvas);
                }

            }
            catch (Exception)
            {

            }
            
            return res;
        }

        public List<PackageInfo> GetInstalledPackages(int flags = 0)
        {
            if(Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                return packageManager.GetInstalledPackages(PackageManager.PackageInfoFlags.Of((long)PackageInfoFlags.MatchAll)).ToList<PackageInfo>();
            }
            else
            {
                return packageManager.GetInstalledPackages(PackageInfoFlags.MatchAll).ToList<PackageInfo>();
            }
        }

        public List<ApplicationInfo> GetInstalledApplication()
        {
            var launchIntent = new Intent(Intent.ActionMain)
                    .AddCategory(Intent.CategoryLauncher);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                return packageManager.QueryIntentActivities(launchIntent, PackageManager.ResolveInfoFlags.Of(0))
                    .Select(x => x.ActivityInfo.ApplicationInfo)
                    .ToList();
                
            }
            else
            {
               return packageManager.QueryIntentActivities(launchIntent,0)
                    .Select(x => x.ActivityInfo.ApplicationInfo)
                    .ToList(); 
            }
        }

        public ApplicationInfo GetApplicationInfo(string packageName, int flags = 0)
        {
            if(Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                return packageManager.GetApplicationInfo(packageName,PackageManager.ApplicationInfoFlags.Of((long)flags));
            }
            else
            {
                return packageManager.GetApplicationInfo(packageName, PackageInfoFlags.MatchAll);
            }
        }

       

        public string GetApplicationName(ApplicationInfo applicationInfo) => packageManager.GetApplicationLabel(applicationInfo);

        public string GetApplicationName(string packageName, int flags = 0)
        {
            using var applicationinfo = GetApplicationInfo(packageName, flags);
            return packageManager.GetApplicationLabel(applicationinfo);
        }

        public Drawable GetApplicationIcon(ApplicationInfo applicationInfo) => packageManager.GetApplicationIcon(applicationInfo);
    }
}
