// # ==============================================================================
// # Solution: BiteRight
// # File: SearchValidator.cs
// # Author: Łukasz Sobczak
// # Created: 20-02-2024
// # ==============================================================================

using BiteRight.Application.Dtos.Common;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Currencies.Search;

public class SearchValidator : AbstractValidator<SearchRequest>
{
    public SearchValidator(
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer,
        IStringLocalizer<Resources.Resources.Common.Common> commonLocalizer
        )
    {
        const int maxQueryLength = 64;
        RuleFor(x => x.Query)
            .MaximumLength(maxQueryLength)
            .WithMessage(string.Format(currenciesLocalizer[Resources.Resources.Currencies.Currencies.query_too_long], maxQueryLength));
        
        RuleFor(x => x.PaginationParams)
            .NotNull()
            .WithMessage(commonLocalizer[Resources.Resources.Common.Common.pagination_params_null]);
        
        RuleFor(x => x.PaginationParams)
            .SetValidator(new PaginationParamsValidator(commonLocalizer));
    }
}