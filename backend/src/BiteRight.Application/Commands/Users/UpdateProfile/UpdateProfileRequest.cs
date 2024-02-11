using System;
using MediatR;

namespace BiteRight.Application.Commands.Users.UpdateProfile;

public class UpdateProfileRequest : IRequest<Unit>
{
    public Guid CurrencyId { get; set; }
    public string TimeZoneId { get; set; }
}