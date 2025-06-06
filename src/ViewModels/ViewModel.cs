﻿using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Monitoring.Common.Settings;
using SukiUI.Dialogs;
using SukiUI.Toasts;

namespace Monitoring.ViewModels;

public interface ISingletonViewModel : IViewModel;

public interface ITransientViewModel : IViewModel;

public interface IViewModel;

public abstract partial class ViewModel : ObservableRecipient
{
    private static readonly Lazy<SukiDialogManager> LazySukiDialogManager = new();
    private static readonly Lazy<SukiToastManager> LazySukiToastManager = new();

    protected ViewModel(AppSettings appSettings)
    {
        AppSettings = appSettings;
    }

    public ISukiDialogManager DialogManager => LazySukiDialogManager.Value;
    public ISukiToastManager ToastManager => LazySukiToastManager.Value;

    public AppSettings AppSettings { get; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsEditable { get; set; }

    public virtual void OnLoaded() { }

    public virtual void OnUnloaded() { }

    /// <summary>
    /// Dispatches the specified action on the UI thread synchronously.
    /// </summary>
    /// <param name="action">The action to be dispatched.</param>
    protected static void Dispatch(Action action) => Dispatcher.UIThread.Invoke(action);

    /// <summary>
    /// Dispatches the specified action on the UI thread synchronously.
    /// </summary>
    /// <param name="action">The action to be dispatched.</param>
    protected static TResult Dispatch<TResult>(Func<TResult> action) =>
        Dispatcher.UIThread.Invoke(action);

    /// <summary>
    /// Dispatches the specified action on the UI thread.
    /// </summary>
    /// <param name="action">The action to be dispatched.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected static async Task DispatchAsync(Action action) =>
        await Dispatcher.UIThread.InvokeAsync(action);

    /// <summary>
    /// Dispatches the specified action on the UI thread asynchronously.
    /// </summary>
    /// <param name="action">The action to be dispatched.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected static async Task<TResult> DispatchAsync<TResult>(Func<TResult> action) =>
        await Dispatcher.UIThread.InvokeAsync(action);

    protected void OnAllPropertiesChanged() => OnPropertyChanged(string.Empty);

    // ~ViewModel() => Dispose(false);
    //
    // /// <inheritdoc cref="Dispose"/>>
    // protected virtual void Dispose(bool disposing) { }
    //
    // /// <inheritdoc />>
    // public void Dispose()
    // {
    //     Dispose(true);
    //     GC.SuppressFinalize(this);
    // }
}
