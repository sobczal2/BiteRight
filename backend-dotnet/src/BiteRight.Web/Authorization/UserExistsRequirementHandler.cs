using System.Threading.Tasks;
using BiteRight.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Web.Authorization;

public class UserExistsRequirementHandler : AuthorizationHandler<UserExistsRequirement>
{
    private readonly AppDbContext _appDbContext;

    public UserExistsRequirementHandler(
        AppDbContext appDbContext
    )
    {
        _appDbContext = appDbContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        UserExistsRequirement requirement
    )
    {
        var userId = context.User.Identity?.Name;
        if (userId is null)
        {
            return;
        }
        
        var user = await _appDbContext
            .Users
            .FirstOrDefaultAsync(x => x.IdentityId == userId);

        if (user is null)
        {
            return;
        }
        
        context.Succeed(requirement);
    }
}