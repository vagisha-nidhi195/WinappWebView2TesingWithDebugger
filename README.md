# WinappWebView2TesingWithDebugger
Windows UWP and WPF app testing with WebView2 and opening up the remote debugger.

# Details
We are using the details on https://docs.microsoft.com/en-us/microsoft-edge/webview2/how-to/webdriverto try an open a debugger port on UWP app, but are facing some challenges in doing so.
I have tested this with two basic apps (UWP and WPF). The solution works for a WPF app but does not for UWP (might be missing implementations in the XAML library)

Versions (Using the latest pre-release versions in UWP):

Microsoft.UI.Xaml: 2.8.0-prerelease.220118001

Microsoft.Web.WebView2: 1.0.1158-prerelease

# Steps to reproduce
There are two ways to open a debugger port in webview.

## Option 1: Use a system variable to define WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS to --remote-debugging-port={port_no}

WPF
   * This succeeds when used with the WPF (Microsoft.Web.WebView2.Wpf) app, but fails to open a port (silently) on a UWP app with *Microsoft.UI.Xaml* library controls.
   * There are two basic apps in the repo.
   * To simulate this, just set the WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS system environments. Make sure to restart the machine as its loaded at startup. If you make any changes on the port number, the computer needs to restart to make affect.
   * Run the BasicWPFAppWithWebView2/../bin/Debug/WebViewHostTest.exe either using VS(local debugger) or using cmd. If you are using VS, there might be other apps that open up a debugger port, so ideally just build and then run the exe from commandline.
   * Navigate to localhost://{port_no}. You can see the html debugger.

UWP
   * Close the WPF and run the BasicUWPAppWithWebView2 app in VS or install it and then run.
   * Navigate to localhost://{port_no}. This fails to load. 

## Option 2 Providing the port as additional browser argument when creating the webview.

WPF 
   * The BasicWPFAppWithWebView2 app already has a port defined in env variables which is passed to the webview when creating it.
   * For this to work, you need to remove the system environments defined above, as by default, it uses the system environments. [Another bug, it should ideally used the one defined using CoreWebView2EnvironmentOptions and default to system env if not provided.]
   * Navigate to *localhost://{port_no}*. You can see the html debugger.

UWP
   * The XAML controls webview2 does not contain an api to refresh/load the webview with a custom **CoreWebView2EnvironmentOptions**: https://docs.microsoft.com/en-us/windows/winui/api/microsoft.ui.xaml.controls.webview2.ensurecorewebview2async?view=winui-3.0


# Expectations
* Remote debugging should work as expected for UWP app as with WPF (by providing WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS system env)
* The **WebView2.EnsureCoreWebView2Async** in microsoft.ui.xaml.controls.webview2 should contain an optional parameter to provide environment options to pass additional arguments similar to WPF: https://docs.microsoft.com/en-us/dotnet/api/microsoft.web.webview2.wpf.webview2.ensurecorewebview2async?view=webview2-dotnet-1.0.1108.44
