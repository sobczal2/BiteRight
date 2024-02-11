using BiteRight.Application.Dtos.Common;
using BiteRight.Domain.Categories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Categories.Search;

public class SearchValidator : AbstractValidator<SearchRequest>
{
    public SearchValidator(
        IStringLocalizer<Resources.Resources.Categories.Categories> categoriesLocalizer,
        IStringLocalizer<Resources.Resources.Common.Common> commonLocalizer
    )
    {
        const int maxQueryLength = 64;
        RuleFor(x => x.Query)
            .MaximumLength(maxQueryLength)
            .WithMessage(string.Format(categoriesLocalizer[nameof(Resources.Resources.Categories.Categories.query_too_long)], maxQueryLength));
        RuleFor(x => x.PaginationParams)
            .NotNull()
            .WithMessage(commonLocalizer[nameof(Resources.Resources.Common.Common.pagination_params_null)]);
        RuleFor(x => x.PaginationParams)
            .SetValidator(new PaginationParamsValidator(commonLocalizer));
    }
}