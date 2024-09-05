using CommunityToolkit.Mvvm.ComponentModel;
using NotificationCompress.Helps;
using NotificationCompress.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCompress.Services
{
    public class RuleAggregator
    {
        private static readonly Lazy<RuleAggregator> _ = new Lazy<RuleAggregator>(() => new RuleAggregator());
        public ObservableCollection<RuleAction> Actions { get; set; } = new ObservableCollection<RuleAction>();
        public List<Message> FilterMessageAggregator { get; set; } = new List<Message>();
        public List<Message> SilenceMessageAggregator { get; set; } = new List<Message>();
        public List<Message> TransmitMessageAggregator { get; set; } = new List<Message>();

        private RuleAggregator() { }

        public static RuleAggregator Instance
        {
            get => _.Value;
        }
    }
}
