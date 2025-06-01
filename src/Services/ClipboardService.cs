using Avalonia.Controls.ApplicationLifetimes;

namespace Monitoring.Services;

public sealed class ClipboardService
{
    private readonly IClassicDesktopStyleApplicationLifetime _lifeTime;

    public ClipboardService(IClassicDesktopStyleApplicationLifetime lifeTime)
    {
        _lifeTime = lifeTime;
    }

    public void CopyToClipboard(string text) => _lifeTime.MainWindow?.Clipboard?.SetTextAsync(text);
}
