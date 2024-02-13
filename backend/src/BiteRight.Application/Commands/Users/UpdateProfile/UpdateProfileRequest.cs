// # ==============================================================================
// # Solution: BiteRight
// # File: UpdateProfileRequest.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using MediatR;

#endregion

namespace BiteRight.Application.Commands.Users.UpdateProfile;

public record UpdateProfileRequest(
    Guid CurrencyId,
    string TimeZoneId
) : IRequest<Unit>;