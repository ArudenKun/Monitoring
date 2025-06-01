using Monitoring.ViewModels;
using Monitoring.Views.Abstractions;

namespace Monitoring.Views;

public sealed partial class MainWindow : SukiWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
