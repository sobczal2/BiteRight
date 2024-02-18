// # ==============================================================================
// # Solution: BiteRight
// # File: DisposeProductsTests.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BiteRight.Application.Commands.Products.Create;
using BiteRight.Application.Dtos.Products;
using BiteRight.Infrastructure.Configuration.Categories;
using BiteRight.Infrastructure.Configuration.Currencies;
using BiteRight.Infrastructure.Configuration.Units;
using BiteRight.Infrastructure.Database;
using BiteRight.Web.Tests.Integration.TestHelpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BiteRight.Web.Tests.Integration.Controllers.Products;

[Collection("DatabaseCollection")]
public class DisposeProductsTests : IAsyncDisposable
{
    private readonly HttpClient _client;
    private readonly BiteRightBackendFactory _biteRightBackendFactory;

    public DisposeProductsTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _biteRightBackendFactory = biteRightBackendFactory;
        _client = biteRightBackendFactory.CreateClient();
    }

    private static string GetUrl(
        Guid productId
    ) =>
        $"api/Products/{productId}/dispose";

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
    
    [Fact]
    public async Task Dispose_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var createRequest = new CreateRequest(
            "Example Product",
            "This is an example product description.",
            19.99m,
            CurrencyConfiguration.USD.Id,
            ExpirationDateKindDto.BestBefore,
            new DateOnly(2025, 12, 31),
            CategoryConfiguration.Dairy.Id,
            100,
            UnitConfiguration.Kilogram.Id
        );
        var createHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Products")
            .AuthorizeAsOnboardedUser();
        createHttpRequestMessage.Content = JsonContent.Create(createRequest);
        
        var createHttpResponse =
            await _client.SendAsync(createHttpRequestMessage);
        createHttpResponse.EnsureSuccessStatusCode();
        
        var createResponse = await createHttpResponse.Content.ReadFromJsonAsync<CreateResponse>();
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, GetUrl(createResponse!.ProductId))
            .AuthorizeAsOnboardedUser();

        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}