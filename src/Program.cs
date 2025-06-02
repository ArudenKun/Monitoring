using System;
using Avalonia;
using Avalonia.Hosting;
using HotAvalonia;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using ZLogger;

namespace Monitoring;

internal static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.AddMonitoring();
        builder.AddAvaloniaHosting<App>(ConfigureAppBuilder);
        var app = builder.Build();
        try
        {
            app.Run();
        }
        catch (Exception ex)
        {
            app.Services.GetRequiredService<ILogger<App>>()
                .ZLogError(ex, $"Application terminated unexpectedly");
            PInvoke.MessageBox(
                new HWND(0),
                ex.Message,
                "Monitoring Fatal Error",
                MESSAGEBOX_STYLE.MB_ICONERROR
            );
            throw;
        }
        finally
        {
            app.Dispose();
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    [UsedImplicitly]
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<Application>().UsePlatformDetect().LogToTrace();

    private static void ConfigureAppBuilder(
        IServiceProvider serviceProvider,
        AppBuilder appBuilder
    ) =>
        appBuilder
            .UseHotReload()
            .UsePlatformDetect()
            .UseR3(e =>
                serviceProvider
                    .GetRequiredService<ILogger<App>>()
                    .ZLogError(e, $"Unhandled exception")
            )
            .LogToTrace();
}
