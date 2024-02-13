// # ==============================================================================
// # Solution: BiteRight
// # File: AuthorizeNamePresent.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace BiteRight.Web.Authorization;

public class AuthorizeNamePresent : AuthorizeAttribute
{
    public AuthorizeNamePresent()
    {
        Policy = Policies.NamePresent;
    }
}