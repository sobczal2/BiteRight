using Microsoft.AspNetCore.Authorization;

namespace BiteRight.Web.Authorization;

public class AuthorizeNamePresent : AuthorizeAttribute
{
    public AuthorizeNamePresent()
    {
        Policy = Policies.NamePresent;
    }
}