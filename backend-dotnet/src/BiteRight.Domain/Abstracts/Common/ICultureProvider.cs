using System.Globalization;

namespace BiteRight.Domain.Abstracts.Common;

public interface ICultureProvider
{
    CultureInfo RequireCurrent();
}