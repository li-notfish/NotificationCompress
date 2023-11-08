using AndroidApplication = Android.App.Application;
using Android.Content.PM;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationCompress.Services;
using System.Collections.ObjectModel;
using NotificationCompress.Models;
using NotificationCompress.Helps;

namespace NotificationCompress.ViewModels
{
    public partial class AppFiltePageViewModel : ObservableRecipient
    {
        private readonly SystemServices systemService;

        [ObservableProperty]
        ObservableCollection<AppShortcut> shortcuts = new ObservableCollection<AppShortcut>();

        public AppFiltePageViewModel(SystemServices systemServices)
        {
            systemService = systemServices;
            InitData();
        }


        public void InitData()
        {
            var installApplications = systemService.GetInstalledApplication();
            Console.WriteLine(installApplications.Any(x => x.LoadLabel(systemService.packageManager) == "QQ"));
            Task.Run(async () =>
            {
                foreach (var item in installApplications)
                {
                    
                    var appshortcut = new AppShortcut();
                    appshortcut.Title = item.LoadLabel(systemService.packageManager);
                    appshortcut.PackageName = item.PackageName;
                    //appshortcut.Icon = DrawBitHelp.GetBitmap(item.ApplicationInfo.LoadIcon(systemService.packageManager));
                    appshortcut.Icon = await LoadImageSourceHelp.LoadImageAsync(appshortcut.IconImage,item);
                    appshortcut.GetImage();
                    Shortcuts.Add(appshortcut);
                }
            });
        }
    }
}
