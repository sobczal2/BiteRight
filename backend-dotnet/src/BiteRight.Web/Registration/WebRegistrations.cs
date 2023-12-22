using System;
using System.Security.Claims;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Options;
using BiteRight.Web.Authorization;
using BiteRight.Web.Filters;
using BiteRight.Web.Middleware;
using BiteRight.Web.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    }

    private static void AddControllers(
        IServiceCollection services
    )
    {
        services.AddControllers(opt => { opt.Filters.Add<ValidationExceptionFilter>(); });
    }

    private static void AddSwagger(
        IServiceCollection services
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
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
        });
    }

    private static void AddAuth(
        IServiceCollection services,
        IConfiguration configuration
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
            .SetDefaultPolicy(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .AddRequirements(new NamePresentRequirement())
                .Build());

        services.AddSingleton<IAuthorizationHandler, NamePresentRequirementHandler>();
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
        services.AddSingleton<IIdentityAccessor, HttpContextIdentityAccessor>();
    }
}