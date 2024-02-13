// # ==============================================================================
// # Solution: BiteRight
// # File: DatabaseCollection.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

using Xunit;

namespace BiteRight.Web.Tests.Integration.TestHelpers;

[CollectionDefinition("DatabaseCollection")]
public class DatabaseCollection : ICollectionFixture<BiteRightBackendFactory>
{
    
}