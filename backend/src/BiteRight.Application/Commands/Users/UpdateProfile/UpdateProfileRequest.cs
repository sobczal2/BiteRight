using MediatR;

namespace BiteRight.Application.Commands.Users.UpdateProfile;

public class UpdateProfileRequest : IRequest<Unit>
{
    public Guid LanguageId { get; set; }
    public Guid CurrencyId { get; set; }
    public Guid CountryId { get; set; }
}