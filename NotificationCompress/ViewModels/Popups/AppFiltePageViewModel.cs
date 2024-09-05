using DroidGraps = Android.Graphics;
using CommunityToolkit.Mvvm.ComponentModel;
using NotificationCompress.Services;
using System.Collections.ObjectModel;
using NotificationCompress.Models;
using NotificationCompress.Helps;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Core;

namespace NotificationCompress.ViewModels.Popups
{
    public partial class AppFilterPageViewModel : ObservableRecipient
    {
        private readonly SystemServices systemService;

        private readonly string storageConfigDir = FileSystem.Current.AppDataDirectory;

        private readonly IconCache iconCache;

        private IPopupService PopupService;

        [ObservableProperty]
        ObservableCollection<AppShortcut> shortcuts = new ObservableCollection<AppShortcut>();

        [ObservableProperty]
        ObservableCollection<AppShortcut> selectShortcuts = new ObservableCollection<AppShortcut>();

        public AppFilterPageViewModel(SystemServices systemServices, IPopupService popupService,IconCache iconCache)
        {
            systemService = systemServices;
            this.iconCache = iconCache;
            PopupService = popupService;
            if(shortcuts.Count == 0)
            {
                InitData();
            }
        }


        public void InitData()
        {
            var installApplications = systemService?.GetInstalledApplication();
            foreach (var item in installApplications)
            {

                var appShortcut = new AppShortcut();
                appShortcut.Title = systemService.GetApplicationName(item);
                appShortcut.PackageName = item.PackageName;
                DroidGraps.Bitmap res = null;
                iconCache.BitmapCache.TryGetValue(item.PackageName, out res);
                if (res == null)
                {
                    res = systemService.GetRawIconBitmap(item.PackageName);
                    iconCache.BitmapCache.Add(item.PackageName, res);
                }
                var buff = BitImageHelp.GetImageStream(res);
                appShortcut.IconBytes = buff;
                Shortcuts.Add(appShortcut);
            }
        }

        [RelayCommand]
        public void AddToListening(AppShortcut appShortcut)
        {
            if(appShortcut.IsChecked)
            {
                SelectShortcuts.Add(appShortcut);
            }
            else
            {
                SelectShortcuts.Remove(appShortcut);
            }
        }

    }
}
