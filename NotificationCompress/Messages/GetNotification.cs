using CommunityToolkit.Mvvm.Messaging.Messages;
using NotificationCompress.Models;

namespace NotificationCompress.Messages
{
    public class GetNotification : ValueChangedMessage<Message>
    {
        public GetNotification(Message message) : base(message)
        {
            
        }
    }
}
