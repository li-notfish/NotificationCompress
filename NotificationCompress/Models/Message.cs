﻿using Android.App;
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
        public Bitmap Icon { get; set; }
        public ImageSource IconImage { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public PendingIntent PendingIntent { get; set; }

        public void GetImage()
        {
            if(Icon == default(Bitmap))
            {
                IconImage = default(ImageSource);
                return;
            }
            var bubbf = BitImageHelp.GetImageStream(Icon);
            IconImage = ImageSource.FromStream(() => new MemoryStream(bubbf));
        }
    }
}
