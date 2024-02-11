using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Dtos.Common;

public class PaginationParamsValidator : AbstractValidator<PaginationParams>
{
    public PaginationParamsValidator(
        IStringLocalizer<Resources.Resources.Common.Common> commonLocalizer
    )
    {
        const int minPageNumber = 0;
        const int minPageSize = 1;
        const int maxPageSize = 100;
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(minPageNumber)
            .WithMessage(string.Format(commonLocalizer[nameof(Resources.Resources.Common.Common.page_number_not_valid)], minPageNumber));
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(minPageSize)
            .WithMessage(string.Format(commonLocalizer[nameof(Resources.Resources.Common.Common.page_size_not_valid)], minPageSize, maxPageSize));
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(maxPageSize)
            .WithMessage(string.Format(commonLocalizer[nameof(Resources.Resources.Common.Common.page_size_not_valid)], minPageSize, maxPageSize));
    }
}