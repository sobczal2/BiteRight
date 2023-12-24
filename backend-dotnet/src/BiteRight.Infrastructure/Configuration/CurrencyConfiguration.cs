using BiteRight.Domain.Currency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(
        EntityTypeBuilder<Currency> builder
    )
    {
        builder.ToTable("currencies", "currency");
        builder.Ignore(currency => currency.DomainEvents);
        builder.HasKey(currency => currency.Id);
        builder.Property(currency => currency.Id)
            .HasConversion(
                id => id.Value,
                value => new CurrencyId(value)
            )
            .ValueGeneratedNever();
        builder.Property(currency => currency.Code)
            .HasConversion(
                code => code.Value,
                value => Code.CreateSkipValidation(value)
            );
        builder.Property(currency => currency.Name)
            .HasConversion(
                name => name.Value,
                value => Name.CreateSkipValidation(value)
            );
        builder.Property(currency => currency.Symbol)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.CreateSkipValidation(value)
            );
    }
}