
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
        // Repositories
        services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
        services.AddScoped<IProjetosRepository, ProjetosRepository>();
        services.AddScoped<IAlunosRepository, AlunosRepository>();
        // UseCases
        services.AddScoped<ColaboradorUseCases>();
        services.AddScoped<ProjetosUseCases>();
        services.AddScoped<AlunosUseCases>();

        // ViewModels
        services.AddScoped<ColaboradorViewModel>();
        services.AddScoped<ProjetosViewModel>();
        services.AddScoped<AlunosViewModel>();

        // Views
        services.AddScoped<MainWindow>();
        // Construção das dependências
        _serviceProvider = services.BuildServiceProvider();

        var scope = _serviceProvider.CreateScope();

        var window = scope.ServiceProvider.GetRequiredService<MainWindow>();

        window.Closed += (s, e) => scope.Dispose();

        window.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();

        base.OnExit(e);
    }
}