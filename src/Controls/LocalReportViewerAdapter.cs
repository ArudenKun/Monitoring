using System.Windows.Forms;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;
using Microsoft.Reporting.WinForms;

namespace Monitoring.Controls;

public partial class LocalReportViewerAdapter : NativeControlHost
{
    private readonly ReportViewer _reportViewer;

    public LocalReportViewerAdapter()
    {
        _reportViewer = new ReportViewer();
        _reportViewer.Dock = DockStyle.Fill;
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        RefreshReport();
        base.OnLoaded(e);
    }

    public void Clear() => _reportViewer.Clear();

    public void Refresh() => _reportViewer.Refresh();

    public void RefreshReport() => _reportViewer.RefreshReport();

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        // Get the Win32 handle
        var handle = _reportViewer.Handle;

        return new PlatformHandle(handle, "ReportViewerHandle");
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        _reportViewer.Clear();
        base.DestroyNativeControlCore(control);
    }
}
