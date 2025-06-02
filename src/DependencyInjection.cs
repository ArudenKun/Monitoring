using System.Text.Json.Serialization.Metadata;
using Humanizer;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Monitoring.Common;
using Monitoring.Common.Extensions;
using Monitoring.Common.Helpers;
using Monitoring.Common.Settings;
using Monitoring.Data;
using Monitoring.ViewModels;
using Monitoring.Views.Abstractions;
using ServiceScan.SourceGenerator;
using ZLogger;
using ZLogger.Providers;

namespace Monitoring;

public static partial class DependencyInjection
{
    public static HostApplicationBuilder AddMonitoring(this HostApplicationBuilder builder)
    {
        builder
            .Logging.ClearProviders()
            .SetMinimumLevel(LogLevel.Debug)
            .AddZLoggerConsole(options => options.UseDefaultPlainTextFormatter())
            .AddZLoggerRollingFile(options =>
            {
                options.FilePathSelector = (timestamp, sequenceNumber) =>
                    PathHelper.LogsDirectory.CombinePath(
                        $"{timestamp.ToLocalTime():yyyy-MM}_{sequenceNumber:000}.log"
                    );
                options.RollingInterval = RollingInterval.Month;
                options.RollingSizeKB = (int)2.Gigabytes().Kilobytes;
                options.UseDefaultPlainTextFormatter();
            });

        builder.Services
            .AddViews()
            .AddViewModels()
            .AddSingleton<ViewLocator>()
            .AddSingleton<IJsonTypeInfoResolver>(AppJsonContext.Default)
            .AddSingleton(AppJsonContext.Default.Options)
            .AddSingleton<AppSettings>()
            .AddLinqToDBContext<AppDbContext>((sp, options) =>
                options.UseSQLiteOfficial(PathHelper.DatabasePath).UseDefaultLogging(sp));


        return builder;
    }

    private static void UseDefaultPlainTextFormatter(this ZLoggerOptions options)
    {
        options.UsePlainTextFormatter(formatter =>
        {
            formatter.SetPrefixFormatter($"[{0} {1,-11}] ",
                (in template, in info) => template.Format(info.Timestamp, info.LogLevel));
        });
    }

    [GenerateServiceRegistrations(
        AssignableTo = typeof(ISingletonViewModel),
        AsSelf = true,
        AsImplementedInterfaces = true,
        Lifetime = ServiceLifetime.Singleton
    )]
    [GenerateServiceRegistrations(
        AssignableTo = typeof(ITransientViewModel),
        AsSelf = true,
        AsImplementedInterfaces = true,
        Lifetime = ServiceLifetime.Transient
    )]
    private static partial IServiceCollection AddViewModels(this IServiceCollection services);

    // [GenerateServiceRegistrations(
    //     AssignableTo = typeof(IView<>),
    //     CustomHandler = nameof(AddViewsHandler)
    // )]

    [GenerateServiceRegistrations(
        AssignableTo = typeof(IView<>),
        AsSelf = true,
        Lifetime = ServiceLifetime.Transient
    )]
    private static partial IServiceCollection AddViews(this IServiceCollection services);

    // private static void AddViewsHandler<
    //     [DynamicallyAccessedMembers(
    //         DynamicallyAccessedMemberTypes.Interfaces
    //         | DynamicallyAccessedMemberTypes.PublicConstructors
    //     )]
    //     TView
    // >(this IServiceCollection services)
    //     where TView : Control
    // {
    //     var type = typeof(TView);
    //     var viewInterfaceType =
    //         type.GetInterface("IView`1")
    //         ?? throw new InvalidOperationException(
    //             $"Type '{type.FullName}' does not implement IView"
    //         );
    //     services.AddTransient<TView>();
    //     services.AddTransient(viewInterfaceType, sp => sp.GetRequiredService<TView>());
    // }
}