// # ==============================================================================
// # Solution: BiteRight
// # File: ListRequest.cs
// # Author: Łukasz Sobczak
// # Created: 12-01-2024
// # ==============================================================================

#region

using MediatR;

#endregion

namespace BiteRight.Application.Queries.Countries.List;

public class ListRequest : IRequest<ListResponse>;