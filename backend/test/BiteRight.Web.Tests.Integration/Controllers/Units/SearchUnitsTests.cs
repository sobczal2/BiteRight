// # ==============================================================================
// # Solution: BiteRight
// # File: SearchUnitsTests.cs
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

namespace BiteRight.Web.Tests.Integration.Controllers.Units;

[Collection("DatabaseCollection")]
public class SearchUnitsTests : IAsyncDisposable
{
    private readonly HttpClient _client;

    public SearchUnitsTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _client = biteRightBackendFactory.CreateClient();
    }

    private static string GetUrl(
        int pageNumber,
        int pageSize,
        string query
    ) =>
        $"api/Units/search?query={query}&pageNumber={pageNumber}&pageSize={pageSize}";

    [Fact]
    public async Task Search_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, GetUrl(pageNumber, pageSize, string.Empty))
            .AuthorizeAsOnboardedUser();

        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public async ValueTask DisposeAsync()
    {
        if (_client is IAsyncDisposable clientAsyncDisposable)
            await clientAsyncDisposable.DisposeAsync();
        else
            _client.Dispose();

        GC.SuppressFinalize(this);
    }
}