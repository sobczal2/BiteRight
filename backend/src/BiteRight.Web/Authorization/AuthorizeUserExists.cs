using Microsoft.AspNetCore.Authorization;

namespace BiteRight.Web.Authorization;

public class AuthorizeUserExists : AuthorizeAttribute
{
    public AuthorizeUserExists()
    {
        Policy = Policies.UserExists;
    }
}