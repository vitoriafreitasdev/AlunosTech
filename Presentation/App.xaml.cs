
using Application.Interfaces;
using Application.UseCases;

using Infrastructure.Data;
using Infrastructure.Repositories;

using Presentation.ViewModels;
using Presentation.Views;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using DotNetEnv;
using System.Windows;

namespace Presentation;

public partial class App : System.Windows.Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Env.Load(@"C:\AlunosTech\AlunosTech\.env");

        var sqlstring =
            Environment.GetEnvironmentVariable("SQLString");

        if (string.IsNullOrWhiteSpace(sqlstring))
        {
            throw new InvalidOperationException(
                "Connection String não encontrada.");
        }

        var services = new ServiceCollection();

        // Banco de dados
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(sqlstring));

        // Infrastructure
        services.AddScoped<
            IColaboradorRepository,
            ColaboradorRepository>();

        // Application
        services.AddScoped<ColaboradorUseCases>();

        // Presentation
        services.AddScoped<ColaboradorViewModel>();
        services.AddScoped<ColaboradorView>();

        // Construção das dependências
        _serviceProvider = services.BuildServiceProvider();

        // Criando um escopo para a janela
        var scope = _serviceProvider.CreateScope();

        // Obtendo a View
        var window = scope.ServiceProvider
            .GetRequiredService<ColaboradorView>();

        window.Closed += (s, args) => scope.Dispose();

        window.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();

        base.OnExit(e);
    }
}