using MarcialArtSchool.Core.RepositoryContracts;
using MartialArtSchool.Infrastructure.DbContext;
using MartialArtSchool.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace MartialArtSchool.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAccountRepository, AccountRepository>();
        services.AddSingleton<IPupilsRepository, PupilsRepository>();
        services.AddSingleton<DapperDbContext>();
        return services;

    }
}

