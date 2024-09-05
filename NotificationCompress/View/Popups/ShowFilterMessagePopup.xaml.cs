using CommunityToolkit.Maui.Views;
using NotificationCompress.ViewModels.Popups;

namespace NotificationCompress.View.Popups;

public partial class ShowFilterMessagePopup : Popup
{
	public ShowFilterMessagePopup(ShowFilterMessagePopupViewModel showFilterMessagePopupViewModel)
	{
		InitializeComponent();

		BindingContext = showFilterMessagePopupViewModel;
	}
}