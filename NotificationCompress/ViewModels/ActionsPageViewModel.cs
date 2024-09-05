using Android.Content.Res;
using Android.Util;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NotificationCompress.Helps;
using NotificationCompress.Messages;
using NotificationCompress.Models;
using NotificationCompress.Services;
using NotificationCompress.View;
using System.Collections.ObjectModel;

namespace NotificationCompress.ViewModels
{
    [QueryProperty(nameof(RuleAction), "RuleAction")]
    public partial class ActionsPageViewModel : ObservableRecipient
    {
        [ObservableProperty]
        ObservableCollection<RuleAction> actionList = new ObservableCollection<RuleAction>();

        private RuleAction RuleAction { get; set; }

        private LocalDatabase localDatabase;

        private RuleAggregator ruleAggregator;

        public ActionsPageViewModel(LocalDatabase localDatabase, RuleAggregator ruleAggregator)
        {
            this.localDatabase = localDatabase;
            this.ruleAggregator = ruleAggregator;
            WeakReferenceMessenger.Default.Register<SendRuleAction>(this, SendRuleActionHandle);
            Task.Run(InitRuleFormDb);

        }


        public async Task InitRuleFormDb()
        {
            var actions = await localDatabase.GetActionsAsync();
            foreach (var action in actions)
            {
                action.RulePkgList = action.RulePkg.Split(',').ToList<string>();
                ruleAggregator.Actions.Add(action);
            }
            ActionList = ruleAggregator.Actions;
        }

        [RelayCommand]
        public async Task AddNewAction()
        {
            await Shell.Current.GoToAsync(nameof(NewActionView));
        }

        private async Task RefreshAction()
        {
            var list = await localDatabase.GetActionsAsync();
            if (list.Count() != 0)
            {
                ruleAggregator.Actions.Clear();
                foreach (var action in list)
                {
                    action.RulePkgList = action.RulePkg.Split(',').ToList<string>();
                    ruleAggregator.Actions.Add(action);
                }
                ActionList = ruleAggregator.Actions;
            }
        }

        private async void SendRuleActionHandle(object sender, SendRuleAction sendRuleActionTuple)
        {
            if(sendRuleActionTuple.Value.Item2 == false)
            {
                return;
            }

            var isSuccessful = await localDatabase.SaveActionAsync(sendRuleActionTuple.Value.Item1);
            if (isSuccessful > 0)
            {
                ruleAggregator.Actions.Add(sendRuleActionTuple.Value.Item1);
                if(sendRuleActionTuple.Value.Item1.RuleType == RuleEnum.Filter)
                {
                    Preferences.Default.Set("ShowFilterTime", 4);
                    ruleAggregator.FilterMessageAggregator.Clear();
                }
            }
        }

    }
}
