using Lucide.Avalonia;
using Monitoring.Common.Settings;

namespace Monitoring.ViewModels.Pages;

public sealed class DashboardPageViewModel : PageViewModel, ITransientViewModel
{
    public DashboardPageViewModel(AppSettings appSettings)
        : base(appSettings) { }

    public override string DisplayName => "Dashboard";
    public override long Order => 0;
    public override LucideIconKind Icon => LucideIconKind.House;
}
