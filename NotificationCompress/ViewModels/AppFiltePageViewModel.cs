using DroidGraps = Android.Graphics;
using CommunityToolkit.Mvvm.ComponentModel;
using NotificationCompress.Services;
using System.Collections.ObjectModel;
using NotificationCompress.Models;
using NotificationCompress.Helps;
using CommunityToolkit.Mvvm.Input;

namespace NotificationCompress.ViewModels
{
    public partial class AppFilterPageViewModel : ObservableRecipient
    {
        private readonly SystemServices systemService;

        private readonly string storageConfigDir = FileSystem.Current.AppDataDirectory;

        private readonly IconCache iconCache = IconCache.Instance;

        private bool isInit = false;

        [ObservableProperty]
        ObservableCollection<AppShortcut> shortcuts = new ObservableCollection<AppShortcut>();

        public AppFilterPageViewModel(SystemServices systemServices)
        {
            systemService = systemServices;
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
                isInit = true;
            }
        }

        private void SaveConfig()
        {
            var IsCheckedList = shortcuts.Where(x => x.IsChecked == true).ToList();

            var targetConfigFile = Path.Combine(storageConfigDir,"config.json");

            using(FileStream outputFileStream = File.Create(targetConfigFile))
            {

            }

           
        }

        [RelayCommand]
        public void AddToListening(AppShortcut appShortcut)
        {
            Shortcuts.Single(x => x.PackageName == appShortcut.PackageName).IsChecked = !appShortcut.IsChecked;
        }

    }
}
