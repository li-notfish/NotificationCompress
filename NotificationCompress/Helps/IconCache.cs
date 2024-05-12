using Android.Graphics;

namespace NotificationCompress.Helps
{
    public class IconCache
    {
        private static readonly Lazy<IconCache> _ = new Lazy<IconCache>(() => new IconCache());

        public Dictionary<string, Bitmap> BitmapCache = new Dictionary<string, Bitmap>();

        private IconCache() { }

        public static IconCache Instance
        {
            get => _.Value;
        }


    }
}
