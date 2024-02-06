using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public class GetPhotoValidator : AbstractValidator<GetPhotoRequest>
{
    private const int MaxPhotoNameLength = 64;

    public GetPhotoValidator(
        IStringLocalizer<Resources.Resources.Categories.Categories> localizer
    )
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage(localizer[nameof(Resources.Resources.Categories.Categories.category_id_empty)]);
    }
}