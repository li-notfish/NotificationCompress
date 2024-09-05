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

    public class SendRuleAction : ValueChangedMessage<Tuple<RuleAction,bool>>
    {
        public SendRuleAction(Tuple<RuleAction,bool> ruleSetCompleted) : base(ruleSetCompleted)
        {
            
        }
    }

    public class SendFilterMessage : ValueChangedMessage<List<Message>>
    {
        public SendFilterMessage(List<Message> filterMessage) : base(filterMessage)
        {
            
        }
    }

    public class SendNextSchedulerMessage : ValueChangedMessage<int>
    {
        public SendNextSchedulerMessage(int nextScheduler) : base(nextScheduler)
        {
            
        }
    }
}
