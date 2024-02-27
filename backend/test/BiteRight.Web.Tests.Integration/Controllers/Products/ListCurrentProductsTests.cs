// # ==============================================================================
// # Solution: BiteRight
// # File: ListCurrentProductsTests.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

#region

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BiteRight.Application.Dtos.Products;
using BiteRight.Infrastructure.Database;
using BiteRight.Web.Tests.Integration.TestHelpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

#endregion

namespace BiteRight.Web.Tests.Integration.Controllers.Products;

[Collection("DatabaseCollection")]
public class ListCurrentProductsTests : IAsyncDisposable
{
    private readonly BiteRightBackendFactory _biteRightBackendFactory;
    private readonly HttpClient _client;

    public ListCurrentProductsTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _biteRightBackendFactory = biteRightBackendFactory;
        _client = biteRightBackendFactory.CreateClient();
    }

    public async ValueTask DisposeAsync()
    {
        if (_client is IAsyncDisposable clientAsyncDisposable)
            await clientAsyncDisposable.DisposeAsync();
        else
            _client.Dispose();

        using var scope = _biteRightBackendFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await ProductsCleanup.Execute(dbContext);

        GC.SuppressFinalize(this);
    }

    private static string GetUrl(
        ProductSortingStrategy sortingStrategy
    )
    {
        return $"api/Products/current?sortingStrategy={sortingStrategy}";
    }

    [Fact]
    public async Task ListCurrent_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var sortingStrategy = ProductSortingStrategy.NameAsc;
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, GetUrl(sortingStrategy))
            .AuthorizeAsOnboardedUser();

        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}