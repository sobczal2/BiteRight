// # ==============================================================================
// # Solution: BiteRight
// # File: ListLanguagesTests.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BiteRight.Web.Tests.Integration.TestHelpers;
using FluentAssertions;
using Xunit;

namespace BiteRight.Web.Tests.Integration.Controllers.Languages;

[Collection("DatabaseCollection")]
public class ListLanguagesTests : IAsyncDisposable
{
    private readonly HttpClient _client;

    public ListLanguagesTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _client = biteRightBackendFactory.CreateClient();
    }

    private static string GetUrl() =>
        "api/Languages";
    
    public async ValueTask DisposeAsync()
    {
        if (_client is IAsyncDisposable clientAsyncDisposable)
            await clientAsyncDisposable.DisposeAsync();
        else
            _client.Dispose();
        
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task List_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, GetUrl())
            .AuthorizeAsOnboardedUser();

        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}