// # ==============================================================================
// # Solution: BiteRight
// # File: ListCurrenciesTests.cs
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

namespace BiteRight.Web.Tests.Integration.Controllers.Currencies;

[Collection("DatabaseCollection")]
public class ListCurrenciesTests : IAsyncDisposable
{
    private readonly HttpClient _client;

    public ListCurrenciesTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _client = biteRightBackendFactory.CreateClient();
    }

    private static string GetUrl() =>
        "api/Currencies";
    
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