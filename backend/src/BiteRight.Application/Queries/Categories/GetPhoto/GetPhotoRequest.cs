using MediatR;

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public class GetPhotoRequest : IRequest<GetPhotoResponse>
{
    public GetPhotoRequest(Guid categoryId)
    {
        CategoryId = categoryId;
    }

    public Guid CategoryId { get; }
}