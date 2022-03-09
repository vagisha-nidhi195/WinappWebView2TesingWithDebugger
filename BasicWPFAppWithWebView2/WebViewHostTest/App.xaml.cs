using System;
using System.Windows;

namespace WebViewHostTest
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var debuggingPort = 4400;
            if (e.Args.Length != 0)
            {
                debuggingPort = Convert.ToInt32(e.Args[0]);
            }

            var wnd = new MainWindow(debuggingPort);
            wnd.Show();
        }
    }
}
