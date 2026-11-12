using Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views;

public partial class ColaboradorView : Window
{
    public ColaboradorView(ColaboradorViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;

    }

    private async void TabControl_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (e.Source is TabControl)
        {
            if (DataContext is ColaboradorViewModel viewModel)
            {
                await viewModel.CarregarColaboradores();
            }
        }
    }
}

//App.xaml linha tirada: StartupUri="MainWindow.xaml"
