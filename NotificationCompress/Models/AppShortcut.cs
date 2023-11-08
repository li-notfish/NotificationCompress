using Android.Graphics;
using Android.Graphics.Drawables;
using NotificationCompress.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCompress.Models
{
    public record AppShortcut
    {
        public string Title { get; set; }
        public Bitmap Icon { get; set ; }
        public string PackageName { get; set; }
        public ImageSource IconImage { get; set; }
        public bool IsChecked { get; set; } = false;

        public AppShortcut()
        {
            
        }

        public AppShortcut(string title, Bitmap icon, string packageName)
        {
            this.Title = title;
            this.Icon = icon;
            this.PackageName = packageName;
        }

        public void GetImage()
        {
            if(Icon == null)
            {
                IconImage = default(ImageSource);
                return;
            }

            var bubbf = BitImageHelp.GetImageStream(Icon);
            IconImage = ImageSource.FromStream(() => new MemoryStream(bubbf));
        }
    }
}
