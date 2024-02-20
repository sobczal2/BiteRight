// # ==============================================================================
// # Solution: BiteRight
// # File: ChangeAmountRequest.cs
// # Author: Łukasz Sobczak
// # Created: 17-02-2024
// # ==============================================================================

using System;
using System.Text.Json.Serialization;
using MediatR;

namespace BiteRight.Application.Commands.Products.ChangeAmount;

public class ChangeAmountRequest : IRequest<Unit>
{
    [JsonIgnore]
    public Guid ProductId { get; set; }
    public double Amount { get; init; }
}