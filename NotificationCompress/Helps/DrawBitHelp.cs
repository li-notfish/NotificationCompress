using Android.Graphics;
using Android.Graphics.Drawables;

namespace NotificationCompress.Helps
{
    public static class DrawBitHelp
    {
        public static Bitmap GetBitmap(Drawable drawable) => drawable is BitmapDrawable bitmapDrawable ? bitmapDrawable.Bitmap : null;
    }
}
