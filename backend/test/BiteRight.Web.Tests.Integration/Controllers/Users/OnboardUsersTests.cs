// # ==============================================================================
// # Solution: BiteRight
// # File: OnboardUsersTests.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BiteRight.Application.Commands.Users.Onboard;
using BiteRight.Infrastructure.Database;
using BiteRight.Web.Tests.Integration.TestHelpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BiteRight.Web.Tests.Integration.Controllers.Users;

[Collection("DatabaseCollection")]
public class OnboardUsersTests : IAsyncDisposable
{
    private readonly HttpClient _client;
    private readonly BiteRightBackendFactory _biteRightBackendFactory;

    public OnboardUsersTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _biteRightBackendFactory = biteRightBackendFactory;
        _client = biteRightBackendFactory.CreateClient();
    }

    private static string GetUrl() =>
        "api/Users/onboard";

    public async ValueTask DisposeAsync()
    {
        if (_client is IAsyncDisposable clientAsyncDisposable)
            await clientAsyncDisposable.DisposeAsync();
        else
            _client.Dispose();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task Onboard_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var request = new OnboardRequest(
            TestUsers.EmailVerifiedUser.Username,
            TimeZoneInfo.Utc.Id
        );
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, GetUrl())
            .AuthorizeAsEmailVerifiedUser();

        httpRequestMessage.Content = JsonContent.Create(request);

        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Cleanup
        using var scope = _biteRightBackendFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext
            .Users
            .Where(u => u.IdentityId == TestUsers.EmailVerifiedUser.IdentityId)
            .ExecuteDeleteAsync();
    }
}