using System.Windows;

using Presentation.ViewModels;
using Presentation.Views;

namespace Presentation
{
    public partial class MainWindow : Window
    {
        private readonly ColaboradorView _colaboradorView;

        private readonly ProjetosView _projetosView;

        private readonly AlunosView _alunosView;

        public MainWindow(
            ColaboradorViewModel colaboradorViewModel,
            ProjetosViewModel projetosViewModel,
            AlunosViewModel alunosViewModel)
        {
            InitializeComponent();

            // VIEW DE COLABORADORES

            _colaboradorView = new ColaboradorView()
            {
                DataContext = colaboradorViewModel
            };

            // VIEW DE PROJETOS

            _projetosView = new ProjetosView()
            {
                DataContext = projetosViewModel
            };

            // VIEW DE ALUNOS

            _alunosView = new AlunosView()
            {
                DataContext = alunosViewModel
            };

            // TELA INICIAL

            MainContent.Content = _colaboradorView;
        }

        // NAVEGAÇÃO: COLABORADORES

        private void Colaboradores_Click(
            object sender,
            RoutedEventArgs e)
        {
            MainContent.Content = _colaboradorView;
        }

        // NAVEGAÇÃO: PROJETOS

        private void Projetos_Click(
            object sender,
            RoutedEventArgs e)
        {
            MainContent.Content = _projetosView;
        }

        // NAVEGAÇÃO: ALUNOS

        private void Alunos_Click(
            object sender,
            RoutedEventArgs e)
        {
            MainContent.Content = _alunosView;
        }
    }
}