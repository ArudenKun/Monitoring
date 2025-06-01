using System;
using Avalonia.Controls;
using Monitoring.ViewModels;

namespace Monitoring.Views.Abstractions;

public abstract class UserControl<TViewModel> : UserControl, IView<TViewModel>
    where TViewModel : ViewModel
{
    public TViewModel ViewModel => DataContext;

    public new TViewModel DataContext
    {
        get =>
            base.DataContext as TViewModel
            ?? throw new InvalidCastException(
                $"DataContext is null or not of the expected type '{typeof(TViewModel).FullName}'."
            );
        set => base.DataContext = value;
    }
}
