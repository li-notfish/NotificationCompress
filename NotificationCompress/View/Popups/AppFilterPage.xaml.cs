using CommunityToolkit.Maui.Views;
using NotificationCompress.ViewModels.Popups;

namespace NotificationCompress.View.Popups;

public partial class AppFilterPage : Popup
{
	private AppFilterPageViewModel appFilterPageViewModel;

	public AppFilterPage(AppFilterPageViewModel viewModel)
	{
		InitializeComponent();
        appFilterPageViewModel = viewModel;
        BindingContext = appFilterPageViewModel;
		ResultWhenUserTapsOutsideOfPopup = appFilterPageViewModel.SelectShortcuts.Select(x => x.PackageName);
	}

    private async void Closed_Clicked(object sender, EventArgs e)
    {
		var pkgList = appFilterPageViewModel.SelectShortcuts.Select(x => x.PackageName).ToList<string>();

        await CloseAsync(pkgList);
    }
}