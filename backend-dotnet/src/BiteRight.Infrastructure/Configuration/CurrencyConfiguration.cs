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
        builder.ToTable("Currencies", "Currency");
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
    }
}