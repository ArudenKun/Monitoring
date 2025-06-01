using Monitoring.ViewModels;

namespace Monitoring.Views.Abstractions;

public interface IView;

public interface IView<out TViewModel> : IView
    where TViewModel : ViewModel
{
    TViewModel ViewModel { get; }
    TViewModel DataContext { get; }
}
