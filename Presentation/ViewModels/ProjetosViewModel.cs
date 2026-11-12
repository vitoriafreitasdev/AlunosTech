
using Application.DTOs;
using Application.UseCases;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    public class ProjetosViewModel : INotifyPropertyChanged
    {
        private readonly ProjetosUseCases _useCases;

        public event PropertyChangedEventHandler? PropertyChanged;

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

        private string _descricao = string.Empty;
        public string Descricao
        {
            get => _descricao;
            set
            {
                _descricao = value;
                OnPropertyChanged(nameof(Descricao));
            }
        }

        private DateTime? _dataDeEntrega;
        public DateTime? DataDeEntrega
        {
            get => _dataDeEntrega;
            set
            {
                _dataDeEntrega = value;
                OnPropertyChanged(nameof(DataDeEntrega));
            }
        }

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

        public ObservableCollection<Projeto> Projetos { get; set; } = new();

        public ICommand CadastrarProjetoCommand { get; }

        public ICommand CarregarProjetosCommand { get; }

        public ProjetosViewModel(ProjetosUseCases useCases)
        {
            _useCases = useCases;

            CadastrarProjetoCommand = new RelayCommand(
                async () => await CadastrarProjeto()
            );

            CarregarProjetosCommand = new RelayCommand(
                async () => await CarregarProjetos()
            );
        }

        private async Task CadastrarProjeto()
        {
            if (string.IsNullOrWhiteSpace(Nome) ||
                string.IsNullOrWhiteSpace(Descricao) ||
                DataDeEntrega == null)
            {
                Mensagem = "Preencha todos os campos.";
                return;
            }

            try
            {
                Projeto projeto = new()
                {
                    Nome = Nome,
                    Descricao = Descricao,
                    DataDeEntrega = DataDeEntrega.Value
                };

                bool resultado = await _useCases.Adicionar(projeto);

                if (!resultado)
                {
                    Mensagem = "Não foi possível cadastrar o projeto.";
                    return;
                }

                Mensagem = "Projeto cadastrado com sucesso!";

                Nome = string.Empty;
                Descricao = string.Empty;
                DataDeEntrega = null;

                await CarregarProjetos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Mensagem = "Erro ao cadastrar projeto.";
            }
        }

        public async Task CarregarProjetos()
        {
            try
            {
                var resultado = await _useCases.ProjetosAdicionados();

                if (!resultado.sucesso)
                {
                    Mensagem = resultado.mensagem;
                    return;
                }

                Projetos.Clear();

                foreach (var projeto in resultado.projetos)
                {
                    Projetos.Add(projeto);
                }

                Mensagem = resultado.mensagem;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Mensagem = "Erro ao carregar projetos.";
            }
        }

        private void OnPropertyChanged(string propriedade)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propriedade)
            );
        }
    }
}