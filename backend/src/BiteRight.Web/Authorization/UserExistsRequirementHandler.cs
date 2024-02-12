// # ==============================================================================
// # Solution: BiteRight
// # File: UserExistsRequirementHandler.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using Microsoft.AspNetCore.Authorization;

#endregion

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