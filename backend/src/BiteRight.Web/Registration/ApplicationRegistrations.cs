using BiteRight.Application.Commands.Users.Onboard;
using BiteRight.Application.Common;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BiteRight.Web.Registration;

public static class ApplicationRegistrations
{
    public static void AddBiteRightApplication(
        IServiceCollection services
    )
    {
        AddMediator(services);
        AddValidators(services);
    }

    private static void AddMediator(
        IServiceCollection services
    )
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining<OnboardRequest>();
            opt.AddOpenBehavior(typeof(MediatorValidationBehaviour<,>));
        });
    }

    private static void AddValidators(
        IServiceCollection services
    )
    {
        services.AddValidatorsFromAssemblyContaining<OnboardRequest>();
    }
}