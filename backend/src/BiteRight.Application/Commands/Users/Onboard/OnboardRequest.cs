// # ==============================================================================
// # Solution: BiteRight
// # File: OnboardRequest.cs
// # Author: Łukasz Sobczak
// # Created: 11-02-2024
// # ==============================================================================

#region

using MediatR;

#endregion

namespace BiteRight.Application.Commands.Users.Onboard;

public record OnboardRequest(
    string Username,
    string TimeZoneId
) : IRequest<Unit>;