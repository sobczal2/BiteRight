// # ==============================================================================
// # Solution: BiteRight
// # File: AuthorizeUserExists.cs
// # Author: Łukasz Sobczak
// # Created: 12-01-2024
// # ==============================================================================

#region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace BiteRight.Web.Authorization;

public class AuthorizeUserExists : AuthorizeAttribute
{
    public AuthorizeUserExists()
    {
        Policy = Policies.UserExists;
    }
}