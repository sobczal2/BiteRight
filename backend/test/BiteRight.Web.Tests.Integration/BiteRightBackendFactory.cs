// # ==============================================================================
// # Solution: BiteRight
// # File: BiteRightBackendFactory.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Linq;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Infrastructure.Database;
using BiteRight.Web.Tests.Integration.Dependencies;
using BiteRight.Web.Tests.Integration.TestHelpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using Xunit;

#endregion

namespace BiteRight.Web.Tests.Integration;

public class BiteRightBackendFactory : WebApplicationFactory<IWebAssemblyMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer;

    public BiteRightBackendFactory()
    {
        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(
        IWebHostBuilder builder
    )
    {
        builder.ConfigureLogging(logging => { logging.ClearProviders(); });

        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });

            services.RemoveAll(typeof(IHostedService));

            services.RemoveAll(typeof(IIdentityManager));
            services.AddSingleton<IIdentityManager, TestIdentityManager>();

            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString());
                options.UseSnakeCaseNamingConvention();
            });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();

            TestUsers.SeedOnboardedUser(dbContext);
        });
    }

    public IServiceScope CreateScope()
    {
        var scope = Services.CreateScope();
        return scope;
    }
}