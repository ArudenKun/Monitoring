namespace Avalonia.Controls;

partial class WebView2
{
    bool _allowExternalDrop = true;

    /// <summary>
    /// Enable/disable external drop.
    /// 启用/禁用外部拖拽。
    /// </summary>
    public bool AllowExternalDrop
    {
        get {
#if _WINDOWS_
            if (_coreWebView2Controller != null)
            {
                return _coreWebView2Controller.AllowExternalDrop;
            }
#else
            // CEF_TODO: 待实现 AllowExternalDrop
#endif
            return _allowExternalDrop; }
        set
        {
            if (_allowExternalDrop == value)
            {
                return;
            }
            _allowExternalDrop = value;
#if _WINDOWS_
            if (_coreWebView2Controller is not null)
            {
                _coreWebView2Controller.AllowExternalDrop = value;
            }
#elif ANDROID
#elif IOS
#elif MACOS
#else
            // CEF_TODO: 待实现 AllowExternalDrop
#endif
        }
    }

    /// <summary>
    /// The <see cref="AvaloniaProperty" /> which backs the <see cref="AllowExternalDropProperty" /> property.
    /// </summary>
    public static readonly DirectProperty<WebView2, bool> AllowExternalDropProperty =
        AvaloniaProperty.RegisterDirect<WebView2, bool>(
            nameof(AllowExternalDrop),
            x => x._allowExternalDrop,
            (x, y) => x.AllowExternalDrop = y
        );
}
