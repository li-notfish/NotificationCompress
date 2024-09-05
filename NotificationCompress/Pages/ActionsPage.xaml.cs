using NotificationCompress.ViewModels;

namespace NotificationCompress.Pages;

public partial class ActionsPage : ContentPage
{
	public ActionsPage(ActionsPageViewModel actionsPageViewModel)
	{
		InitializeComponent();
		BindingContext = actionsPageViewModel;
	}
}