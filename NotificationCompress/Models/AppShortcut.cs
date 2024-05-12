using Android.Graphics;

namespace NotificationCompress.Models
{
    public record AppShortcut
    {
        public string Title { get; set; }
        public string PackageName { get; set; }
        public byte[] IconBytes { get; set; }
        public bool IsChecked { get; set; } = false;

        public AppShortcut()
        {
            
        }

        public AppShortcut(string title, Bitmap icon, string packageName)
        {
            this.Title = title;
            this.PackageName = packageName;
        }
    }
}
