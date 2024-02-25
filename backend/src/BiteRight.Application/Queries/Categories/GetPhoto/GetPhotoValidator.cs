// # ==============================================================================
// # Solution: BiteRight
// # File: GetPhotoValidator.cs
// # Author: Łukasz Sobczak
// # Created: 11-02-2024
// # ==============================================================================

#region

using FluentValidation;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public class GetPhotoValidator : AbstractValidator<GetPhotoRequest>
{
    public GetPhotoValidator(
        IStringLocalizer<Resources.Resources.Categories.Categories> categoriesLocalizer
    )
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage(categoriesLocalizer[nameof(Resources.Resources.Categories.Categories.category_id_empty)]);
    }
}