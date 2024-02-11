using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Users;

namespace BiteRight.Domain.Abstracts.Common;

public interface IIdentityManager
{
    Task<(Email email, bool isVerified)> GetEmail(
        IdentityId identityId,
        CancellationToken cancellationToken = default
    );
}