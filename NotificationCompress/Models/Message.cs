using Android.App;
using Android.Graphics;
using AndroidX.Core.Content;
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
        public ImageSource IconImage { get => GetImage(); }
        public string Title { get; set; }
        public string Content { get; set; }
        public PendingIntent PendingIntent { get; set; }

        private ImageSource GetImage()
        {
            var pm = AndroidApplication.Context.CreatePackageContext(PackageName,0);
            var mIcon = Task.Run(async () => await BitmapFactory.DecodeResourceAsync(pm.Resources, Icon)).Result;
            var stream = BitImageHelp.GetImageStream(mIcon);
            pm.Dispose();
            if(mIcon.IsRecycled != true)
            {
                mIcon.Recycle();    
            }
            return ImageSource.FromStream(() => stream);
        }
    }
}
