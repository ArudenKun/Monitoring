using System;
using System.Collections.Concurrent;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Monitoring.Common.Helpers;
using Monitoring.ViewModels;
using Monitoring.Views.Abstractions;
using ZLinq;

namespace Monitoring;

public sealed class ViewLocator : IDataTemplate
{
    private static readonly ConcurrentDictionary<Type, Type?> ViewTypeCache = new();

    private static readonly TextBlock ViewNotFoundControl = new();

    public Control Build(object? data) =>
        data is ViewModel viewModel ? TryBindView(viewModel) : ViewNotFoundControl;

    public bool Match(object? data) => data is ViewModel;

    public static Control TryBindView(ViewModel viewModel)
    {
        if (TryCreateView(viewModel) is not { } view)
        {
            ViewNotFoundControl.Text = $"Could not find view for {viewModel.GetType().FullName}";
            return ViewNotFoundControl;
        }

        view.DataContext ??= viewModel;
        BindEvents(view, viewModel);
        return view;
    }

    private static Control? TryCreateView(ViewModel viewModel)
    {
        var vmType = viewModel.GetType();
        var viewType = ViewTypeCache.GetOrAdd(vmType, FindViewTypeForViewModel);
        return viewType is null ? null : Activator.CreateInstance(viewType) as Control;
    }

    private static Type? FindViewTypeForViewModel(Type vmType) =>
        AssemblyHelper
            .GetViewTypes()
            .AsValueEnumerable()
            .Select(candidateViewType =>
                (
                    ViewType: candidateViewType,
                    Interface: candidateViewType
                        .GetInterfaces()
                        .FirstOrDefault(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IView<>)
                        )
                )
            )
            .Where(t => t.Interface is not null)
            .Select(t => (t.ViewType, ViewModelType: t.Interface!.GetGenericArguments()[0]))
            .FirstOrDefault(t => t.ViewModelType.IsAssignableFrom(vmType))
            .ViewType;

    public static void BindEvents(Control control, ViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        ArgumentNullException.ThrowIfNull(control);

        control.Loaded += Loaded;
        control.Unloaded += Unloaded;
        return;

        void Loaded(object? sender, RoutedEventArgs e) => viewModel.OnLoaded();

        void Unloaded(object? sender, RoutedEventArgs e)
        {
            viewModel.OnUnloaded();
            control.Loaded -= Loaded;
            control.Unloaded -= Unloaded;
        }
    }
}
