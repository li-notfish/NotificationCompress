using Android.App;
using Android.Content;
using NotificationCompress.Helps;
using NotificationCompress.ViewModels;
using Application = Android.App.Application;

namespace NotificationCompress.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

    }

}
