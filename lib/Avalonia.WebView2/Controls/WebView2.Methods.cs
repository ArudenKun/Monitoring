using System.Net.Http;
using System.Threading.Tasks;

namespace Avalonia.Controls;

partial class WebView2
{
    /// <summary>
    /// Renders the provided HTML as the top level document of the WebView2.
    /// This is equivalent to CoreWebView2.NavigateToString.
    /// </summary>
    /// <exception cref="InvalidOperationException">The underlying WebView2.CoreWebView2 is not yet initialized.</exception>
    /// <exception cref="InvalidOperationException">Thrown when browser process has unexpectedly and left this control in an invalid state. We are considering throwing a different type of exception for this case in the future.</exception>
    /// <remarks>The htmlContent parameter may not be larger than 2 MB (2 * 1024 * 1024 bytes) in total size. The origin of the new page is about:blank.</remarks>
    public void NavigateToString(string htmlContent)
    {
#if _WINDOWS_
        VerifyBrowserNotCrashedGuard();
        CoreWebView2?.NavigateToString(htmlContent);
#elif ANDROID
        var aWebView = AWebView;
        if (aWebView != null)
        {
            aWebView.LoadData(htmlContent, "text/html", "UTF-8");
        }
#elif IOS
        var wkWebView = WKWebView;
        if (wkWebView != null)
        {
            wkWebView.LoadHtmlString(htmlContent, (NSUrl?)null!);
        }
#else
        // CEF_TODO: 待实现 NavigateToString
#endif
    }

    public void Navigate(string uri)
    {
#if _WINDOWS_
        VerifyBrowserNotCrashedGuard();
        CoreWebView2?.Navigate(uri);
#elif ANDROID
        var aWebView = AWebView;
        if (aWebView != null)
        {
            aWebView.LoadUrl(uri);
        }
#elif IOS
        var wkWebView = WKWebView;
        if (wkWebView != null)
        {
            NSUrl nsUrl = new(uri);
            NSUrlRequest nsUrlRequest = new(nsUrl);
            wkWebView.LoadRequest(nsUrlRequest);
        }
#else
        // CEF_TODO: 待实现 Navigate
#endif
    }

    public void Navigate(
        string uri,
        Stream? content,
        HttpMethod? method = null,
        string? headers = null
    )
    {
#if _WINDOWS_
        VerifyBrowserNotCrashedGuard();
        if (CoreWebView2 != null)
        {
            var req = CoreWebView2.Environment.CreateWebResourceRequest(
                uri,
                WebResourceRequestUri.GetMethod(method),
                content,
                headers
            );
            CoreWebView2.NavigateWithWebResourceRequest(req);
        }
#else
        // CEF_TODO: 待实现 Navigate
#endif
    }

    /// <summary>
    /// Reloads the top level document of the <see cref="WebView2" />.
    /// This is equivalent to CoreWebView2.Reload.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">The underlying WebView2.CoreWebView2 is not yet initialized.</exception>
    /// <exception cref="System.InvalidOperationException">Thrown when browser process has unexpectedly and left this control in an invalid state. We are considering throwing a different type of exception for this case in the future.</exception>
    public void Reload()
    {
#if _WINDOWS_
        VerifyBrowserNotCrashedGuard();
        CoreWebView2?.Reload();
#else
        // CEF_TODO: 待实现 Reload
#endif
    }

    /// <summary>
    /// Stops any in progress navigation in the <see cref="WebView2" />.
    /// This is equivalent to CoreWebView2.Stop.
    /// If the underlying WebView2.CoreWebView2 is not yet initialized, this method does nothing.
    /// </summary>
    public void Stop()
    {
#if _WINDOWS_
        VerifyBrowserNotCrashedGuard();
        CoreWebView2?.Stop();
#else
        // CEF_TODO: 待实现 Stop
#endif
    }

    /// <summary>
    /// Evaluates the script that is specified by script.
    /// </summary>
    /// <param name="script">A script to evaluate.</param>
    public async void Eval(string script)
    {
        // 兼容 .NET MAUI IWebView API
        await ExecuteScriptAsync(script);
    }

    /// <summary>
    /// On platforms that support JavaScript evaluation, evaluates script.
    /// </summary>
    /// <param name="script">The script to evaluate.</param>
    /// <returns>A task that contains the result of the evaluation as a string.</returns>
    public Task<string?> EvaluateJavaScriptAsync(string script)
    {
        // 兼容 .NET MAUI IWebView API
        var result = ExecuteScriptAsync(script);
        return result;
    }

    /// <summary>
    /// Executes the provided script in the top level document of the <see cref="WebView2" />.
    /// This is equivalent to CoreWebView2.ExecuteScriptAsync.
    /// </summary>
    /// <exception cref="InvalidOperationException">The underlying WebView2.CoreWebView2 is not yet initialized.</exception>
    /// <exception cref="InvalidOperationException">Thrown when browser process has unexpectedly and left this control in an invalid state. We are considering throwing a different type of exception for this case in the future.</exception>
    public Task<string?> ExecuteScriptAsync(string script)
    {
#if _WINDOWS_
        VerifyBrowserNotCrashedGuard();
        if (CoreWebView2 != null)
        {
            return CoreWebView2.ExecuteScriptAsync(script);
        }
#else
        // CEF_TODO: 待实现 ExecuteScriptAsync
#endif
        return Task.FromResult((string?)null);
    }

    ///// <summary>
    ///// For internal use by the .NET MAUI platform.
    ///// Raised after web navigation begins.
    ///// </summary>
    //public bool Navigating(WebNavigationEvent evnt, string url)
    //{
    //    // 兼容 .NET MAUI IWebView API
    //    throw new NotImplementedException();
    //}

    ///// <summary>
    ///// For internal use by the .NET MAUI platform.
    ///// Raised after web navigation completes.
    ///// </summary>
    //public void Navigated(WebNavigationEvent evnt, string url, WebNavigationResult result)
    //{
    //    // 兼容 .NET MAUI IWebView API
    //    throw new NotImplementedException();
    //}

    //public void ProcessTerminated(WebProcessTerminatedEventArgs args)
    //{
    //    // 兼容 .NET MAUI IWebView API
    //    throw new NotImplementedException();
    //}
}
