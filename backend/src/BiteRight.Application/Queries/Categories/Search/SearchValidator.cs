using BiteRight.Application.Dtos.Common;
using BiteRight.Domain.Categories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Categories.Search;

public class SearchValidator : AbstractValidator<SearchRequest>
{
    public SearchValidator(
        IStringLocalizer<Resources.Resources.Categories.Categories> localizer,
        IStringLocalizer<Resources.Resources.Common.Common> commonLocalizer
    )
    {
        const int maxQueryLength = 64;
        RuleFor(x => x.Query)
            .MaximumLength(maxQueryLength)
            .WithMessage(string.Format(localizer["query_too_long"], maxQueryLength));
        RuleFor(x => x.PaginationParams)
            .NotNull()
            .WithMessage(commonLocalizer["pagination_params_null"]);
        RuleFor(x => x.PaginationParams)
            .SetValidator(new PaginationParamsValidator(commonLocalizer));
    }
}