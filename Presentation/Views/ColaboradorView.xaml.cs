using System.Windows.Controls;
using Presentation.ViewModels;

namespace Presentation.Views
{
    public partial class ColaboradorView : UserControl
    {
        public ColaboradorView()
        {
            InitializeComponent();
        }

        private async void TabControl_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl &&
                DataContext is ColaboradorViewModel viewModel)
            {
                await viewModel.CarregarColaboradores();
            }
        }
    }
}
//App.xaml linha tirada: StartupUri="MainWindow.xaml"
