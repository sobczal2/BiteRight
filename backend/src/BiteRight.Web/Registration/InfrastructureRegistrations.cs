using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Common;
using BiteRight.Infrastructure.Auth0Management;
using BiteRight.Infrastructure.Common;
using BiteRight.Infrastructure.Database;
using BiteRight.Infrastructure.Domain.Repositories;
using BiteRight.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IFileProvider = BiteRight.Domain.Abstracts.Common.IFileProvider;

namespace BiteRight.Web.Registration;

public static class InfrastructureRegistrations
{
    public static void AddBiteRightInfrastructure(
        IServiceCollection services,
        ConfigurationManager configuration,
        IHostEnvironment environment
    )
    {
        AddDatabase(services, configuration, environment);
        AddCommon(services);
        AddAuth0Management(services);
        AddRepositories(services);
        AddFileProvider(services, configuration);
    }

    private static void AddDatabase(
        IServiceCollection services,
        ConfigurationManager configuration,
        IHostEnvironment environment
    )
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            if (environment.IsDevelopment())
            {
                opt.EnableSensitiveDataLogging();
            }

            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            opt.UseSnakeCaseNamingConvention();
        });
    }

    private static void AddCommon(
        IServiceCollection services
    )
    {
        services.AddSingleton<IDomainEventPublisher, MediatorDomainEventPublisher>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
    }

    private static void AddAuth0Management(
        IServiceCollection services
    )
    {
        services.AddSingleton<IIdentityManager, Auth0IdentityManager>();
    }

    private static void AddRepositories(
        IServiceCollection services
    )
    {
        services.AddScoped<IUserRepository, CachedEfCoreUserRepository>();
        services.AddScoped<ILanguageRepository, CachedEfCoreLanguageRepository>();
        services.AddScoped<ICategoryRepository, CachedEfCoreCategoryRepository>();
        services.AddScoped<ICurrencyRepository, CachedEfCoreCurrencyRepository>();
        services.AddScoped<ICountryRepository, CachedEfCoreCountryRepository>();
        services.AddScoped<IProductRepository, EfCoreProductRepository>();
    }

    private static void AddFileProvider(
        IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.AddSingleton<IFileProvider, FileSystemFileProvider>();
        services.Configure<FileSystemFileProviderOptions>(
            configuration.GetSection(FileSystemFileProviderOptions.SectionName));
    }
}