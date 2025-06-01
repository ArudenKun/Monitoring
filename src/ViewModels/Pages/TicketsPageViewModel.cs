using System;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Input;
using Lucide.Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Monitoring.Common.Settings;

namespace Monitoring.ViewModels.Pages;

public sealed partial class TicketsPageViewModel : PageViewModel, ITransientViewModel
{
    // private readonly CompositeDisposable _disposables;
    private readonly ILogger<TicketsPageViewModel> _logger;

    public TicketsPageViewModel(
        AppSettings appSettings,
        IServiceProvider serviceProvider,
        ILogger<TicketsPageViewModel> logger
    )
        : base(appSettings)
    {
        _logger = logger;
        // _disposables = new CompositeDisposable();
        WebView2CompatViewModel = serviceProvider.GetRequiredService<WebView2CompatViewModel>();
        // TextBoxUrl = new BindableReactiveProperty<string>(WebView2CompatViewModel.Url);
        // TextBoxUrl
        //     .Debounce(1.Seconds())
        //     .ObserveOnUIThreadDispatcher()
        //     .Subscribe(v => WebView2CompatViewModel.Url = v)
        //     .AddTo(_disposables);
    }

    public override void OnUnloaded()
    {
        // _disposables.Dispose();
    }

    public override string DisplayName => "Tickets";
    public override long Order => 1;
    public override LucideIconKind Icon => LucideIconKind.Ticket;

    public WebView2CompatViewModel WebView2CompatViewModel { get; }

    public string Query { get; set; } = string.Empty;

    // public BindableReactiveProperty<string> TextBoxUrl { get; }

    public string PdfPath => @"C:\Users\alden\Downloads\Documents\standardnormaltable.pdf";

    [RelayCommand]
    private void Submit(KeyEventArgs? args)
    {
        if (string.IsNullOrEmpty(Query))
        {
            return;
        }

        IsBusy = true;

        if (args is not null && args.Key == Key.Enter && args.KeyModifiers != KeyModifiers.Shift)
        {
            _logger.LogInformation(
                "Key Pressed: {Key} with Modifiers: {Modifiers}",
                args.Key,
                args.KeyModifiers
            );
            WebView2CompatViewModel.Url = Query;
            args.Handled = true;
        }
        else
        {
            WebView2CompatViewModel.Url = Query;
        }

        IsBusy = false;
    }
}
