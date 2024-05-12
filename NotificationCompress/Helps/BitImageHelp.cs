using Android.Graphics;

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
