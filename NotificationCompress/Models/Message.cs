using Android.App;

namespace NotificationCompress.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PackageName { get; set; }
        public byte[] IconBytes { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public PendingIntent PendingIntent { get; set; }

    }
}
