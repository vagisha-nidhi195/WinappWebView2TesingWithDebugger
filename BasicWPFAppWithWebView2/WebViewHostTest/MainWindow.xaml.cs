using System.IO;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using Path = System.IO.Path;

namespace WebViewHostTest
{
    public partial class MainWindow : Window
    {
        private const string AppUrl = "Sample.html";

        public MainWindow(int remoteDebuggingPort)
        {
            InitializeComponent();
            InitializeWebViewAsync(17978);
        }

        private async void InitializeWebViewAsync(int remoteDebuggingPort)
        {
            CoreWebView2EnvironmentOptions options = new CoreWebView2EnvironmentOptions
            {
                AdditionalBrowserArguments = $"--remote-debugging-port={remoteDebuggingPort}"
            };
            var webViewEnvironment = await CoreWebView2Environment.CreateAsync(null, null, options);

            await myWebBrowser.EnsureCoreWebView2Async(webViewEnvironment);

            myWebBrowser.CoreWebView2.Settings.IsScriptEnabled = true;
            myWebBrowser.CoreWebView2.Settings.IsWebMessageEnabled = true;
            myWebBrowser.CoreWebView2.Settings.AreDevToolsEnabled = true;

            var appUrl = Path.Combine(Directory.GetCurrentDirectory(), AppUrl);
            myWebBrowser.CoreWebView2.Navigate(appUrl);
        }
    }
}
