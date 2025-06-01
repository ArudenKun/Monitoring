using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Monitoring.Common.Settings;
using Monitoring.ViewModels.Pages;
using ZLinq;

namespace Monitoring.ViewModels;

public sealed partial class MainWindowViewModel : ViewModel, ISingletonViewModel
{
    public MainWindowViewModel(AppSettings appSettings, IEnumerable<IPageViewModel> pages)
        : base(appSettings)
    {
        Pages = [.. pages.AsValueEnumerable().Cast<PageViewModel>()];
        ActivePage = Pages[0];
    }

    public IReadOnlyList<PageViewModel> Pages { get; }

    [ObservableProperty]
    public partial PageViewModel ActivePage { get; set; }

    [RelayCommand]
    private void ShowSettings()
    {
        var settings = Pages
            .AsValueEnumerable()
            .FirstOrDefault(page => page is SettingsPageViewModel);

        if (settings is null)
            return;

        ActivePage = settings;
    }
}
