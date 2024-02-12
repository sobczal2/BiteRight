// # ==============================================================================
// # Solution: BiteRight
// # File: GetPhotoCategoriesControllerTests.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace BiteRight.Web.Tests.Integration.CategoriesController;

public class GetPhotoCategoriesControllerTests : IClassFixture<BiteRightBackendFactory>
{
    private readonly HttpClient _client;

    public GetPhotoCategoriesControllerTests(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _client = biteRightBackendFactory.CreateClient();
    }

    [Fact]
    public async Task GetPhoto_ReturnsPhoto_WhenCategoryIdIsValid()
    {
        // Arrange
        var categoryId = Guid.Parse("E8C78317-70AC-4051-805E-ECE2BB37656F"); // Diary
        
        // Act
        var httpResponse = await _client.GetAsync($"api/Categories/{categoryId}/photo");
        
        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}