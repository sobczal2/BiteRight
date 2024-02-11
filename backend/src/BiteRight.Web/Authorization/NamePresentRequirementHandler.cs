using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BiteRight.Web.Authorization;

public class NamePresentRequirementHandler : AuthorizationHandler<NamePresentRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        NamePresentRequirement requirement
    )
    {
        if (!string.IsNullOrWhiteSpace(context.User.Identity?.Name)) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}