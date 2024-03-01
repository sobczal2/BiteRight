// # ==============================================================================
// # Solution: BiteRight
// # File: OnboardRequest.cs
// # Author: Łukasz Sobczak
// # Created: 11-02-2024
// # ==============================================================================

#region

using System;
using MediatR;

#endregion

namespace BiteRight.Application.Commands.Users.Onboard;

public record OnboardRequest(
    string Username,
    Guid CurrencyId,
    string TimeZoneId
) : IRequest<OnboardResponse>;