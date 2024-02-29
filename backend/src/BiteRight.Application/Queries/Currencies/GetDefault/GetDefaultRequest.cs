// # ==============================================================================
// # Solution: BiteRight
// # File: GetDefaultRequest.cs
// # Author: Łukasz Sobczak
// # Created: 29-02-2024
// # ==============================================================================

using MediatR;

namespace BiteRight.Application.Queries.Currencies.GetDefault;

public class GetDefaultRequest : IRequest<GetDefaultResponse>;