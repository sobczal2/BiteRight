// # ==============================================================================
// # Solution: BiteRight
// # File: MeRequest.cs
// # Author: Łukasz Sobczak
// # Created: 12-01-2024
// # ==============================================================================

#region

using MediatR;

#endregion

namespace BiteRight.Application.Queries.Users.Me;

public class MeRequest : IRequest<MeResponse>;