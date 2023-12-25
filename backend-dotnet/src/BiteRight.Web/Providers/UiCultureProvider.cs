using System.Globalization;
using BiteRight.Domain.Abstracts.Common;

namespace BiteRight.Web.Providers;

public class UiCultureProvider : ICultureProvider
{
    public CultureInfo RequireCurrent()
    {
        return CultureInfo.CurrentUICulture;
    }
}