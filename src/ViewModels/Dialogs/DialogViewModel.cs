using Monitoring.Common.Settings;

namespace Monitoring.ViewModels.Dialogs;

public abstract class DialogViewModel : ViewModel
{
    protected DialogViewModel(AppSettings appSettings)
        : base(appSettings) { }
}
