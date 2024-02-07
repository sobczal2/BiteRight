using MediatR;

namespace BiteRight.Application.Commands.Users.Onboard;

public record OnboardRequest(
    string Username,
    string TimeZoneId
) : IRequest<Unit>;