// # ==============================================================================
// # Solution: BiteRight
// # File: CreateProductsTests.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

#region

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

#endregion

namespace BiteRight.Web.Tests.Integration.Controllers.Products;

[Collection("DatabaseCollection")]
public class CreateProductsTests : IAsyncDisposable
{
    private readonly BiteRightBackendFactory _biteRightBackendFactory;
    private readonly HttpClient _client;

    public CreateProductsTests(
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

    private static string GetUrl()
    {
        return "api/Products";
    }

    [Fact]
    public async Task Create_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var createRequest = new CreateRequest(
            "Example Product",
            "This is an example product description.",
            19.99,
            CurrencyConfiguration.USD.Id,
            ExpirationDateKindDto.BestBefore,
            new DateOnly(2025, 12, 31),
            CategoryConfiguration.Dairy.Id,
            100,
            UnitConfiguration.Kilogram.Id
        );
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, GetUrl())
            .AuthorizeAsOnboardedUser();
        httpRequestMessage.Content = JsonContent.Create(createRequest);

        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}