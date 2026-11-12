using Application.DTOs;
using Application.UseCases;
using Domain.Entities;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Presentation.ViewModels;

public class ColaboradorViewModel : INotifyPropertyChanged
{
    private readonly ColaboradorUseCases _useCases;

    public ObservableCollection<Colaborador> Colaboradores { get; set; } = new();

    public ICommand CarregarColaboradoresCommand { get; }
    public ColaboradorViewModel(ColaboradorUseCases useCases)
    {
        _useCases = useCases;

        CadastrarCommand = new RelayCommand(
            async () => await CadastrarColaborador()
        );

        CarregarColaboradoresCommand = new RelayCommand(async () =>
        await CarregarColaboradores());
    }

    // Propriedades da tela

    private string _nome = string.Empty;
    public string Nome
    {
        get => _nome;
        set
        {
            _nome = value;
            OnPropertyChanged();
        }
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    private string _cargo = string.Empty;
    public string Cargo
    {
        get => _cargo;
        set
        {
            _cargo = value;
            OnPropertyChanged();
        }
    }

    private string _salario = string.Empty;
    public string Salario
    {
        get => _salario;
        set
        {
            _salario = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _dataContratacao = DateTime.Today;
    public DateTime? DataContratacao
    {
        get => _dataContratacao;
        set
        {
            _dataContratacao = value;
            OnPropertyChanged();
        }
    }

    private string _mensagem = string.Empty;
    public string Mensagem
    {
        get => _mensagem;
        set
        {
            _mensagem = value;
            OnPropertyChanged();
        }
    }

    // Comando do botão
    public RelayCommand CadastrarCommand { get; }

    public async Task CarregarColaboradores()
    {
        try
        {
            var resultado = await _useCases.RetornarColaboradores();

            if (!resultado.sucesso)
            {
                Mensagem = resultado.mensagem;
                return;
            }

            Colaboradores.Clear();

            foreach (var colaborador in resultado.colaboradores)
            {
                Colaboradores.Add(colaborador);
            }

            Mensagem = resultado.mensagem;
        }
        catch (Exception ex)
        {
            Mensagem = "Erro ao carregar colaboradores.";
            Console.WriteLine(ex);
        }
    }

    // Método responsável pelo cadastro
    private async Task CadastrarColaborador()
    {
        if (string.IsNullOrWhiteSpace(Nome) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Cargo) ||
            string.IsNullOrWhiteSpace(Salario) ||
            DataContratacao == null)
        {
            Mensagem = "Preencha todos os campos.";
            return;
        }

        if (!decimal.TryParse(
            Salario,
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out decimal salario))
        {
            Mensagem = "Informe um salário válido.";
            return;
        }

        if (salario < 0)
        {
            Mensagem = "O salário não pode ser negativo.";
            return;
        }

        try
        {
            Colaborador colaborador = new()
            {
                Nome = Nome,
                Email = Email,
                Cargo = Cargo,
                Salario = salario,
                DataContratacao = DataContratacao.Value
            };

            var resultado = await _useCases.Adicionar(colaborador);

            Mensagem = resultado.mensagem;

            if (resultado.adicionado)
            {
                LimparCampos();
            }
        }
        catch (Exception ex)
        {
            Mensagem = "Erro ao cadastrar colaborador.";
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    private void LimparCampos()
    {
        Nome = string.Empty;
        Email = string.Empty;
        Cargo = string.Empty;
        Salario = string.Empty;
        DataContratacao = DateTime.Today;
    }

    // Notificação de alterações na interface

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}