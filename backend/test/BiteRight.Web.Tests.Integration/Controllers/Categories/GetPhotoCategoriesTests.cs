// # ==============================================================================
// # Solution: BiteRight
// # File: GetPhotoCategoriesControllerTests.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BiteRight.Infrastructure.Configuration.Categories;
using FluentAssertions;
using Xunit;

#endregion

namespace BiteRight.Web.Tests.Integration.Controllers.Categories;

[Collection("DatabaseCollection")]
public class GetPhotoCategoriesTests : IAsyncDisposable
{
    private readonly HttpClient _client;

    public GetPhotoCategoriesTests(
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

    private static string GetUrl(
        Guid categoryId
    )
    {
        return $"api/Categories/{categoryId}/photo";
    }

    [Fact]
    public async Task GetPhoto_ReturnsOK_WhenCategoryIdIsValid()
    {
        // Arrange
        var categoryId = CategoryConfiguration.Dairy.Id;

        // Act
        var httpResponse = await _client.GetAsync(GetUrl(categoryId));

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}