using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BiteRight.Web.Authorization;

public class UserExistsRequirementHandler : AuthorizationHandler<UserExistsRequirement>
{
    private readonly IUserRepository _userRepository;

    public UserExistsRequirementHandler(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        UserExistsRequirement requirement
    )
    {
        var userId = context.User.Identity?.Name;
        if (userId is null) return;

        var user = await _userRepository.FindByIdentityId(userId);

        if (user is null) return;

        context.Succeed(requirement);
    }
}