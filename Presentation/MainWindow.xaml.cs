using System.Windows;
using Presentation.ViewModels;
using Presentation.Views;

namespace Presentation
{
    public partial class MainWindow : Window
    {
        private readonly ColaboradorView _colaboradorView;
        private readonly ProjetosView _projetosView;

        public MainWindow(
            ColaboradorViewModel colaboradorViewModel,
            ProjetosViewModel projetosViewModel)
        {
            InitializeComponent();

            // Criando as Views
            _colaboradorView = new ColaboradorView()
            {
                DataContext = colaboradorViewModel
            };

            _projetosView = new ProjetosView()
            {
                DataContext = projetosViewModel
            };

            // Tela inicial
            MainContent.Content = _colaboradorView;
        }

        private void Colaboradores_Click(
            object sender,
            RoutedEventArgs e)
        {
            MainContent.Content = _colaboradorView;
        }

        private void Projetos_Click(
            object sender,
            RoutedEventArgs e)
        {
            MainContent.Content = _projetosView;
        }
    }
}