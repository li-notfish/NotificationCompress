using AndroidX.Work;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using NotificationCompress.Messages;
using NotificationCompress.Models;
using NotificationCompress.Pages;
using NotificationCompress.Services;
using NotificationCompress.ViewModels;
using System.Collections.ObjectModel;

namespace NotificationCompress.View;

public partial class NewActionView : ContentPage
{
	private SystemServices systemServices;
	private AppFilterPageViewModel filterPageViewModel;
	public RuleAction RuleAction { get; set; } = new RuleAction();

	public NewActionView()
	{
		InitializeComponent();
		BindingContext = RuleAction;
		systemServices = Application.Current.Handler.MauiContext.Services.GetService<SystemServices>();
		filterPageViewModel = Application.Current.Handler.MauiContext.Services.GetService<AppFilterPageViewModel>();
	}

    private async void OpenAppList(object sender, EventArgs e)
    {
		var appFilterPopup = new AppFilterPage(filterPageViewModel);
        if (await this.ShowPopupAsync(appFilterPopup) is List<string> result)
        {
			RuleAction.RulePkg = string.Join(',', result);
            RuleAction.RulePkgList = result;
        }
    }

    private async void Closed_Clicked(object sender, EventArgs e)
	{
		Tuple<RuleAction, bool> tuple = new Tuple<RuleAction, bool>(RuleAction,false);

		WeakReferenceMessenger.Default.Send<SendRuleAction>(new SendRuleAction(tuple));
		await Shell.Current.GoToAsync("..");
	}

    private async void Complete_Clicked(object sender,EventArgs e)
	{
        Tuple<RuleAction, bool> tuple = new Tuple<RuleAction, bool>(RuleAction, true);
		if(RuleAction.RuleType == Helps.RuleEnum.Filter)
		{
	
		}
        WeakReferenceMessenger.Default.Send<SendRuleAction>(new SendRuleAction(tuple));
        await Shell.Current.GoToAsync("..");
    }
}