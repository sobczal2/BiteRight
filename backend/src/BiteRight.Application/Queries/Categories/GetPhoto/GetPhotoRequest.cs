using MediatR;

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public class GetPhotoRequest : IRequest<GetPhotoResponse>
{
    public Guid CategoryId { get; }
    
    public GetPhotoRequest(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}