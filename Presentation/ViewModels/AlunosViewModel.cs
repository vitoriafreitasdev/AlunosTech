using Application.DTOs;
using Application.UseCases;
using Domain.Enums;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    public class AlunosViewModel : INotifyPropertyChanged
    {
        private readonly AlunosUseCases _useCases;

        public event PropertyChangedEventHandler? PropertyChanged;

        // Nome
        private string _nome = string.Empty;

        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }

        // Email
        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        // Ano letivo
        private string _anoLetivo = string.Empty;

        public string AnoLetivo
        {
            get => _anoLetivo;
            set
            {
                _anoLetivo = value;
                OnPropertyChanged(nameof(AnoLetivo));
            }
        }

        // Desempenho
        private DesempenhoCategorias? _desempenho;

        public DesempenhoCategorias? Desempenho
        {
            get => _desempenho;
            set
            {
                _desempenho = value;
                OnPropertyChanged(nameof(Desempenho));
            }
        }

        // Categorias disponíveis no Enum
        public DesempenhoCategorias[] CategoriasDesempenho =>
            Enum.GetValues<DesempenhoCategorias>();

        // Data de matrícula
        private DateTime? _dataDeMatriculacao;

        public DateTime? DataDeMatriculacao
        {
            get => _dataDeMatriculacao;
            set
            {
                _dataDeMatriculacao = value;
                OnPropertyChanged(nameof(DataDeMatriculacao));
            }
        }

        // Mensagem
        private string _mensagem = string.Empty;

        public string Mensagem
        {
            get => _mensagem;
            set
            {
                _mensagem = value;
                OnPropertyChanged(nameof(Mensagem));
            }
        }

        // Lista de alunos
        public ObservableCollection<Aluno> Alunos { get; set; } = new();

        // Commands
        public ICommand CadastrarAlunoCommand { get; }

        public ICommand CarregarAlunosCommand { get; }

        public AlunosViewModel(AlunosUseCases useCases)
        {
            _useCases = useCases;

            CadastrarAlunoCommand = new RelayCommand(
                async () => await CadastrarAluno()
            );

            CarregarAlunosCommand = new RelayCommand(
                async () => await CarregarAlunos()
            );
        }

        // Cadastro de aluno
        private async Task CadastrarAluno()
        {
            if (string.IsNullOrWhiteSpace(Nome) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(AnoLetivo) ||
                Desempenho == null ||
                DataDeMatriculacao == null)
            {
                Mensagem = "Preencha todos os campos.";
                return;
            }

            try
            {
                Aluno aluno = new()
                {
                    Nome = Nome,
                    Email = Email,
                    AnoLetivo = AnoLetivo,
                    Desempenho = Desempenho.Value,
                    DataDeMatriculacao = DataDeMatriculacao.Value
                };

                bool resultado = await _useCases.Adicionar(aluno);

                if (!resultado)
                {
                    Mensagem = "Não foi possível cadastrar o aluno.";
                    return;
                }

                Mensagem = "Aluno cadastrado com sucesso!";

                // Limpar formulário
                Nome = string.Empty;
                Email = string.Empty;
                AnoLetivo = string.Empty;
                Desempenho = null;
                DataDeMatriculacao = null;

                // Atualizar listagem
                await CarregarAlunos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Mensagem = "Erro ao cadastrar aluno.";
            }
        }

        // Carregar alunos cadastrados
        public async Task CarregarAlunos()
        {
            try
            {
                var resultado = await _useCases.AlunosAdicionados();

                if (!resultado.sucesso)
                {
                    Mensagem = resultado.mensagem;
                    return;
                }

                Alunos.Clear();

                foreach (var aluno in resultado.alunos ?? new List<Aluno>())
                {
                    Alunos.Add(aluno);
                }

                Mensagem = resultado.mensagem;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Mensagem = "Erro ao carregar alunos.";
            }
        }

        // Notificação de alterações
        private void OnPropertyChanged(string propriedade)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propriedade)
            );
        }
    }
}