using Android.Graphics;
using Android.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCompress.Helps
{
    public static class DrawBitHelp
    {
        public static Bitmap GetBitmap(Drawable drawable) => drawable is BitmapDrawable bitmapDrawable ? bitmapDrawable.Bitmap : null;
    }
}
