using NotificationCompress.View;

namespace NotificationCompress
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NewActionView),typeof(NewActionView));
        }

    }
}
