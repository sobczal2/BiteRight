// # ==============================================================================
// # Solution: BiteRight
// # File: MeUsersTests.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

using System;
using System.Net.Http;
using System.Threading.Tasks;
using BiteRight.Web.Tests.Integration.TestHelpers;
using Xunit;

namespace BiteRight.Web.Tests.Integration.Controllers.Users;

[Collection("DatabaseCollection")]
public class MeUsersTests : IAsyncDisposable
{
    private readonly HttpClient _client;
    private readonly BiteRightBackendFactory _biteRightBackendFactory;

    public MeUsersTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _biteRightBackendFactory = biteRightBackendFactory;
        _client = biteRightBackendFactory.CreateClient();
    }
    
    private static string GetUrl() =>
        "api/Users/me";
    
    public async ValueTask DisposeAsync()
    {
        if (_client is IAsyncDisposable clientAsyncDisposable)
            await clientAsyncDisposable.DisposeAsync();
        else
            _client.Dispose();

        GC.SuppressFinalize(this);
    }
    
    [Fact]
    public async Task Me_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, GetUrl())
            .AuthorizeAsOnboardedUser();

        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.EnsureSuccessStatusCode();
    }
}