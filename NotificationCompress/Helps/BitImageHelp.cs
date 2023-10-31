using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCompress.Helps
{
    public static class BitImageHelp
    {
        public static byte[] GetImageStream(Bitmap bitmap)
        {
            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png,100,stream);
            return stream.GetBuffer();
        }
    }
}
