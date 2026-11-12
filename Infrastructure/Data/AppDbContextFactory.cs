using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;
namespace Infrastructure.Data;

public class AppDbContextFactory :
    IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        Env.Load(@"C:\AlunosTech\AlunosTech\.env");
        var sqlstring =
            Environment.GetEnvironmentVariable("SQLString");

        if (string.IsNullOrWhiteSpace(sqlstring))
        {
            throw new InvalidOperationException(
                "Connection String não encontrada.");
        }

        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(sqlstring);

        return new AppDbContext(optionsBuilder.Options);
    }
}