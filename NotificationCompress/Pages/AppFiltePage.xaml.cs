using NotificationCompress.ViewModels;

namespace NotificationCompress.Pages;

public partial class AppFiltePage : ContentPage
{
	public AppFiltePage(AppFiltePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}