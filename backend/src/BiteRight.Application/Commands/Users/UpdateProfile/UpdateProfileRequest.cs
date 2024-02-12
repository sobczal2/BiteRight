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

public class UpdateProfileRequest : IRequest<Unit>
{
    public Guid CurrencyId { get; set; }
    public string TimeZoneId { get; set; }
}