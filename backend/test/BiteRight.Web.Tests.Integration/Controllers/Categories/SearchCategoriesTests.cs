// # ==============================================================================
// # Solution: BiteRight
// # File: SearchCategoriesControllerTests.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

#region

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Queries.Categories.Search;
using BiteRight.Web.Tests.Integration.TestHelpers;
using FluentAssertions;
using Xunit;

#endregion

namespace BiteRight.Web.Tests.Integration.Controllers.Categories;

[Collection("DatabaseCollection")]
public class SearchCategoriesTests : IAsyncDisposable
{
    private readonly HttpClient _client;

    public SearchCategoriesTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _client = biteRightBackendFactory.CreateClient();
    }

    public async ValueTask DisposeAsync()
    {
        if (_client is IAsyncDisposable clientAsyncDisposable)
            await clientAsyncDisposable.DisposeAsync();
        else
            _client.Dispose();

        GC.SuppressFinalize(this);
    }

    private static string GetUrl()
    {
        return $"api/Categories/search";
    }

    [Fact]
    public async Task Search_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, GetUrl())
            .AuthorizeAsOnboardedUser();

        httpRequestMessage.Content = JsonContent.Create(new SearchRequest
        {
            PaginationParams = new PaginationParams(pageNumber, pageSize)
        });


        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}