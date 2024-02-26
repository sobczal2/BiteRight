// # ==============================================================================
// # Solution: BiteRight
// # File: EditHandler.cs
// # Author: Łukasz Sobczak
// # Created: 26-02-2024
// # ==============================================================================

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Infrastructure.Database;

namespace BiteRight.Application.Commands.Products.Edit;

public class EditHandler : CommandHandlerBase<EditRequest, EditResponse>
{
    public EditHandler(
        AppDbContext appDbContext
    )
        : base(appDbContext)
    {
    }

    protected override Task<EditResponse> HandleImpl(
        EditRequest request,
        CancellationToken cancellationToken
    )
    {
        throw new System.NotImplementedException();
    }
}