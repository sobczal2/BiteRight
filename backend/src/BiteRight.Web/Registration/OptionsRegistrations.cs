using BiteRight.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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