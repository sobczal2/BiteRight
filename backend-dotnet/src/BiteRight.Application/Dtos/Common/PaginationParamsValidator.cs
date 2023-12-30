using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Dtos.Common;

public class PaginationParamsValidator : AbstractValidator<PaginationParams>
{
    public PaginationParamsValidator(
        IStringLocalizer<Resources.Resources.Common.Common> localizer
    )
    {
        var minPageNumber = 0;
        var minPageSize = 1;
        var maxPageSize = 100;
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(minPageNumber)
            .WithMessage(string.Format(localizer["page_number_not_valid"], minPageNumber));
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(minPageSize)
            .WithMessage(string.Format(localizer["page_size_not_valid"], minPageSize, maxPageSize));
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(maxPageSize)
            .WithMessage(string.Format(localizer["page_size_not_valid"], minPageSize, maxPageSize));
    }
}