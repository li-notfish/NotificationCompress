using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using NotificationCompress.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidApplication = Android.App.Application;

namespace NotificationCompress.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PackageName { get; set; }
        public int Icon { get; set; }
        public ImageSource IconImage { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public PendingIntent PendingIntent { get; set; }

        public void GetImage()
        {
            Bitmap mIcon = default;
            if (Icon == 0)
            {
                IconImage = default;
                return;
            }
            var pm = AndroidApplication.Context.CreatePackageContext(PackageName,Android.Content.PackageContextFlags.IgnoreSecurity);
            var drawable = ContextCompat.GetDrawable(pm, Icon);
            //mIcon = await BitmapFactory.DecodeResourceAsync(pm.Resources, Icon);
            if(drawable is BitmapDrawable bitmapDrawable && bitmapDrawable is not null)
            {
                mIcon = bitmapDrawable.Bitmap;
            }
            if(mIcon is default(Bitmap))
            {
                IconImage = default;
                return;
            }
            var bubbf = BitImageHelp.GetImageStream(mIcon);
            IconImage = ImageSource.FromStream(() => new MemoryStream(bubbf));

        }
    }
}
