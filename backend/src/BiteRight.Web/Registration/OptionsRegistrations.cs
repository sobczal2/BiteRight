// # ==============================================================================
// # Solution: BiteRight
// # File: OptionsRegistrations.cs
// # Author: Łukasz Sobczak
// # Created: 12-01-2024
// # ==============================================================================

#region

using BiteRight.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace BiteRight.Web.Registration;

public static class OptionsRegistrations
{
    public static void AddBiteRightOptions(
        IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.Configure<Auth0Options>(configuration.GetSection(Auth0Options.SectionName));
    }
}