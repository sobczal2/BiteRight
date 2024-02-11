using BiteRight.Application.Dtos.Common;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Units.Search;

public class SearchValidator : AbstractValidator<SearchRequest>
{
    public SearchValidator(
        IStringLocalizer<Resources.Resources.Categories.Categories> unitsLocalizer,
        IStringLocalizer<Resources.Resources.Common.Common> commonLocalizer
    )
    {
        const int maxQueryLength = 64;
        RuleFor(x => x.Query)
            .MaximumLength(maxQueryLength)
            .WithMessage(string.Format(unitsLocalizer[Resources.Resources.Units.Units.query_too_long], maxQueryLength));
        RuleFor(x => x.PaginationParams)
            .NotNull()
            .WithMessage(commonLocalizer[Resources.Resources.Common.Common.pagination_params_null]);
        RuleFor(x => x.PaginationParams)
            .SetValidator(new PaginationParamsValidator(commonLocalizer));
    }
}