using Avalonia.Interactivity;
using Monitoring.ViewModels;
using Monitoring.Views.Abstractions;

namespace Monitoring.Views;

public partial class WebView2Compat : UserControl<WebView2CompatViewModel>
{
    public WebView2Compat()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        DataContext.SetWebView2(WebView2);
        base.OnLoaded(e);
    }
}
