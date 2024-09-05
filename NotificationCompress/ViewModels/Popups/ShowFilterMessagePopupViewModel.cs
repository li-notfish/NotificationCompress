using NotificationCompress.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Collections;
using NotificationCompress.Messages;
using CommunityToolkit.Mvvm.Messaging;
using NotificationCompress.Services;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Android.Content;

namespace NotificationCompress.ViewModels.Popups
{
    public partial class ShowFilterMessagePopupViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private ObservableGroupedCollection<string, List<Message>> filterGroupMessage = new ObservableGroupedCollection<string, List<Message>>();

        private RuleAggregator ruleAggregator = RuleAggregator.Instance;

        public ShowFilterMessagePopupViewModel()
        {
            WeakReferenceMessenger.Default.Register<SendFilterMessage>(this,FilterGroupMessageHandle);
        }

        private void FilterGroupMessageHandle(object s, SendFilterMessage sendFilterMessage)
        {
            var filterMessages = sendFilterMessage.Value;
            var groupMessages = filterMessages.GroupBy(x => x.Name);
            foreach (var groupMessage in groupMessages) 
            {
                var group = new ObservableGroup<string,List<Message>>(groupMessage.Key);
                group.Add(groupMessage.ToList());
                FilterGroupMessage.Add(group);
            }
            
        }

        [RelayCommand]
        private void NextScheduler()
        {
            ruleAggregator.FilterMessageAggregator.Clear();
        }

        [RelayCommand]
        private void OpenMessageIntent(Message message)
        {
            
        }
    }
}
