// # ==============================================================================
// # Solution: BiteRight
// # File: MeResponse.cs
// # Author: Łukasz Sobczak
// # Created: 11-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Users;

#endregion

namespace BiteRight.Application.Queries.Users.Me;

public record MeResponse(UserDto User);