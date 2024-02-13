// # ==============================================================================
// # Solution: BiteRight
// # File: ListRequest.cs
// # Author: Łukasz Sobczak
// # Created: 11-02-2024
// # ==============================================================================

#region

using MediatR;

#endregion

namespace BiteRight.Application.Queries.Currencies.List;

public class ListRequest : IRequest<ListResponse>;