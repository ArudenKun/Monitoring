using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;

namespace Monitoring.Services;

public sealed class ClipboardService
{
    private readonly IClassicDesktopStyleApplicationLifetime _lifeTime;

    public ClipboardService(IClassicDesktopStyleApplicationLifetime lifeTime)
    {
        _lifeTime = lifeTime;
    }

    public void CopyTextToClipboard(string text) =>
        _lifeTime.MainWindow?.Clipboard?.SetTextAsync(text);

    public async ValueTask<string> GetTextFromClipboard()
    {
        var clipboard = _lifeTime.MainWindow?.Clipboard;
        return clipboard is null ? string.Empty : await clipboard.GetTextAsync() ?? string.Empty;
    }
}
