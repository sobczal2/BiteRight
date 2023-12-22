using BiteRight.Domain.Users;

namespace BiteRight.Domain.Abstracts.Common;

public interface IIdentityManager
{
    Task<(Email email, bool isVerified)> GetEmail(
        IdentityId identityId
    );
}