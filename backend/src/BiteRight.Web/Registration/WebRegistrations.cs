using System;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Options;
using BiteRight.Web.Authorization;
using BiteRight.Web.Filters;
using BiteRight.Web.Middleware;
using BiteRight.Web.Providers;
using BiteRight.Web.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BiteRight.Web.Registration;

public static class WebRegistrations
{
    public static void AddBiteRightWeb(
        IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        AddControllers(services);
        AddSwagger(services);
        AddAuth(services, configuration);
        AddMiddlewares(services);
        AddCommon(services);
        AddLocalization(services);
        AddOptions(services, configuration);
    }

    private static void AddControllers(
        IServiceCollection services
    )
    {
        services.AddControllers(opt => { opt.Filters.Add<ApplicationExceptionFilter>(); })
            .AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; });
    }

    private static void AddSwagger(
        IServiceCollection services
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.CustomSchemaIds(type => type.FullName);
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    []
                }
            });

            opt.OperationFilter<CultureQueryParameterFilter>();
            opt.OperationFilter<ProducesInternalServerErrorResponseFilter>();
        });
    }

    private static void AddAuth(
        IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        var auth0Options = configuration.GetSection("Auth0").Get<Auth0Options>();
        if (auth0Options is null)
        {
            throw new InvalidOperationException("Auth0 options are not configured.");
        }

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.Authority = $"https://{auth0Options.Domain}/";
                opt.Audience = auth0Options.Audience;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy(
                Policies.UserExists,
                policy => policy.Requirements.Add(new UserExistsRequirement())
            )
            .AddPolicy(
                Policies.NamePresent,
                policy => policy.Requirements.Add(new NamePresentRequirement())
            );

        services.AddScoped<IAuthorizationHandler, NamePresentRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, UserExistsRequirementHandler>();
    }

    private static void AddMiddlewares(
        IServiceCollection services
    )
    {
        services.AddScoped<CorrelationIdMiddleware>();
        services.AddScoped<TransactionMiddleware>();
    }

    private static void AddCommon(
        IServiceCollection services
    )
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ICorrelationIdAccessor, HttpContextCorrelationIdAccessor>();
        services.AddScoped<IIdentityProvider, HttpContextIdentityProvider>();
        services.AddScoped<ICultureProvider, UiCultureProvider>();
        services.AddScoped<ILanguageProvider, CultureLanguageProvider>();
        services.AddMemoryCache();
    }

    private static void AddLocalization(
        IServiceCollection services
    )
    {
        services.AddLocalization(opt => opt.ResourcesPath = "");
        services.Configure<RequestLocalizationOptions>(opt =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("pl"),
                new CultureInfo("de")
            };
            opt.DefaultRequestCulture = new RequestCulture("en");
            opt.SupportedCultures = supportedCultures;
            opt.SupportedUICultures = supportedCultures;
        });
    }

    private static void AddOptions(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<Auth0Options>(configuration.GetSection(Auth0Options.SectionName));
        services.Configure<CacheOptions>(configuration.GetSection(CacheOptions.SectionName));
    }
}