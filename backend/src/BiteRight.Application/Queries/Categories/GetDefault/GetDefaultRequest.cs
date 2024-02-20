// # ==============================================================================
// # Solution: BiteRight
// # File: GetDefaultRequest.cs
// # Author: Łukasz Sobczak
// # Created: 19-02-2024
// # ==============================================================================

using MediatR;

namespace BiteRight.Application.Queries.Categories.GetDefault;

public class GetDefaultRequest : IRequest<GetDefaultResponse>;