using BiteRight.Domain.Currencies;
using BiteRight.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration.Currencies;

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
                value => value
            )
            .ValueGeneratedNever();

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

        builder.Property(currency => currency.ISO4217Code)
            .HasConversion(
                iso4217Code => iso4217Code.Value,
                value => ISO4217Code.CreateSkipValidation(value)
            );

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Currency> GetSeedData()
    {
        yield return PLN;
        yield return GBP;
        yield return EUR;
        yield return USD;
    }

    // ReSharper disable once InconsistentNaming
    public static Currency PLN { get; } = Currency.Create(
        Name.Create("Polski złoty"),
        Symbol.Create("zł"),
        ISO4217Code.Create("PLN"),
        new SeedDomainEventFactory(),
        new CurrencyId(Guid.Parse("3B56A6DE-3B41-4B10-934F-469CA12F4FE3"))
    );

    // ReSharper disable once InconsistentNaming
    public static Currency GBP { get; } = Currency.Create(
        Name.Create("Pound sterling"),
        Symbol.Create("£"),
        ISO4217Code.Create("GBP"),
        new SeedDomainEventFactory(),
        new CurrencyId(Guid.Parse("53DFFAB5-429D-4626-B1D9-F568119E069A"))
    );

    // ReSharper disable once InconsistentNaming
    public static Currency EUR { get; } = Currency.Create(
        Name.Create("Euro"),
        Symbol.Create("€"),
        ISO4217Code.Create("EUR"),
        new SeedDomainEventFactory(),
        new CurrencyId(Guid.Parse("8B0A0882-3EB5-495A-A646-06D7E0E9FE99"))
    );

    // ReSharper disable once InconsistentNaming
    public static Currency USD { get; } = Currency.Create(
        Name.Create("United States dollar"),
        Symbol.Create("$"),
        ISO4217Code.Create("USD"),
        new SeedDomainEventFactory(),
        new CurrencyId(Guid.Parse("E862F33F-A04A-4B4E-A4BB-9542B1DB3EEB"))
    );
}