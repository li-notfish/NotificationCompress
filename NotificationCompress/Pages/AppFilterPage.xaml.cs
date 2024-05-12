using NotificationCompress.ViewModels;

namespace NotificationCompress.Pages;

public partial class AppFilterPage : ContentPage
{
	public AppFilterPage(AppFilterPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}