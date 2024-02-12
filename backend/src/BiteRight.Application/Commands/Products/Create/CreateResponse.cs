// # ==============================================================================
// # Solution: BiteRight
// # File: CreateResponse.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Application.Commands.Products.Create;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateResponse(Guid ProductId);