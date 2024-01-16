using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Common;
using BiteRight.Infrastructure.Auth0Management;
using BiteRight.Infrastructure.Common;
using BiteRight.Infrastructure.Database;
using BiteRight.Infrastructure.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        services.AddSingleton<IDomainEventFactory, MediatorDomainEventFactory>();
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
        services.AddScoped<IUserRepository, EfCoreUserRepository>();
        services.AddScoped<ILanguageRepository, CachedEfCoreLanguageRepository>();
        services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
        services.AddScoped<ICurrencyRepository, CachedEfCoreCurrencyRepository>();
        services.AddScoped<ICountryRepository, CachedEfCoreCountryRepository>();
    }
}