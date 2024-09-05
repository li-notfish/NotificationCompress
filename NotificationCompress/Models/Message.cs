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


        public Message()
        {
            
        }

        public Message(int id, string name, string packageName, string title, string content, PendingIntent pendingIntent)
        {
            Id = id;
            Name = name;
            PackageName = packageName;
            Title = title;
            Content = content;
            PendingIntent = pendingIntent;
        }

        public static Message Build(int id,string name, string packageName,string title, string content, PendingIntent pendingIntent) => 
            new Message(id,name, packageName, title, content, pendingIntent);
    }
}
