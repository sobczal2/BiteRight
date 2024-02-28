// # ==============================================================================
// # Solution: BiteRight
// # File: TestAuthHandler.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

#endregion

namespace BiteRight.Web.Tests.Integration.TestHelpers;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string HeaderName = "x-test-name";

    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    )
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var name = Context.Request.Headers[HeaderName];
        if (string.IsNullOrEmpty(name)) return Task.FromResult(AuthenticateResult.NoResult());
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, name)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}