using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Monitoring.Common.Extensions;
using Monitoring.Common.Helpers;
using Monitoring.Common.Settings;

namespace Monitoring.ViewModels;

public sealed partial class WebView2CompatViewModel : ViewModel, ITransientViewModel
{
    public WebView2CompatViewModel(AppSettings appSettings)
        : base(appSettings)
    {
        CreationProperties = new CoreWebView2CreationProperties()
        {
            UserDataFolder = PathHelper.DataDirectory.CombinePath("webview"),
        };
    }

    public CoreWebView2CreationProperties CreationProperties { get; }

    public WebView2 WebView2 { get; private set; } = null!;

    [ObservableProperty]
    public partial string Url { get; set; } = "https://google.com";

    // public BindableReactiveProperty<string> Url { get; }

    public void SetWebView2(WebView2 webView2)
    {
        WebView2 = webView2;
    }
}
