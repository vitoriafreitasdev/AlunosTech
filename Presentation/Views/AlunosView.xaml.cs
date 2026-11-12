using System.Windows.Controls;
using Presentation.ViewModels;

namespace Presentation.Views
{
    public partial class AlunosView : UserControl
    {
        public AlunosView()
        {
            InitializeComponent();
        }

        private async void TabControl_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl &&
                DataContext is AlunosViewModel viewModel)
            {
                await viewModel.CarregarAlunos();
            }
        }
    }
}