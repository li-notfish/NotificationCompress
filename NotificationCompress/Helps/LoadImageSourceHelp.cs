using AndroidApplication = Android.App.Application;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;


namespace NotificationCompress.Helps
{
    public class LoadImageSourceHelp
    {
        public static async Task<Bitmap> LoadImageAsync(ImageSource imageSource, ApplicationInfo applicationInfo)
        {
            var packageName = applicationInfo.PackageName;
            using(var drawable = applicationInfo.LoadIcon(AndroidApplication.Context.PackageManager))
            {
                Bitmap bitmap = null;
                await Task.Run(() =>
                {
                    bitmap = Bitmap.CreateBitmap(drawable.IntrinsicWidth, drawable.IntrinsicHeight, Bitmap.Config.Argb8888);
                    using (var canvas = new Canvas(bitmap))
                    {
                        drawable.SetBounds(0,0,canvas.Width,canvas.Height);
                        drawable.Draw(canvas);
                    }
                });
                return bitmap;
            }
        }
    }
}
