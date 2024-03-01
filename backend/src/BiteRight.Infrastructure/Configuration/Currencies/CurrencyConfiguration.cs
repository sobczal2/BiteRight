// # ==============================================================================
// # Solution: BiteRight
// # File: CurrencyConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using BiteRight.Domain.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace BiteRight.Infrastructure.Configuration.Currencies;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    // ReSharper disable once InconsistentNaming
    public static Currency PLN { get; } = Currency.Create(
        Name.Create("Polski złoty"),
        Symbol.Create("zł"),
        ISO4217Code.Create("PLN"),
        false,
        new CurrencyId(Guid.Parse("3B56A6DE-3B41-4B10-934F-469CA12F4FE3"))
    );

    // ReSharper disable once InconsistentNaming
    public static Currency GBP { get; } = Currency.Create(
        Name.Create("Pound sterling"),
        Symbol.Create("£"),
        ISO4217Code.Create("GBP"),
        false,
        new CurrencyId(Guid.Parse("53DFFAB5-429D-4626-B1D9-F568119E069A"))
    );

    // ReSharper disable once InconsistentNaming
    public static Currency EUR { get; } = Currency.Create(
        Name.Create("Euro"),
        Symbol.Create("€"),
        ISO4217Code.Create("EUR"),
        false,
        new CurrencyId(Guid.Parse("8B0A0882-3EB5-495A-A646-06D7E0E9FE99"))
    );

    // ReSharper disable once InconsistentNaming
    public static Currency USD { get; } = Currency.Create(
        Name.Create("United States dollar"),
        Symbol.Create("US$"),
        ISO4217Code.Create("USD"),
        true,
        new CurrencyId(Guid.Parse("E862F33F-A04A-4B4E-A4BB-9542B1DB3EEB"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency JPY { get; } = Currency.Create(
        Name.Create("Japanese yen"),
        Symbol.Create("¥"),
        ISO4217Code.Create("JPY"),
        false,
        new CurrencyId(Guid.Parse("776C1E32-3A9D-4E91-869F-81E89BC17465"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency AUD { get; } = Currency.Create(
        Name.Create("Australian dollar"),
        Symbol.Create("A$"),
        ISO4217Code.Create("AUD"),
        false,
        new CurrencyId(Guid.Parse("5EFC5C7C-872D-4559-A3F3-ECC53F8AE59E"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency CAD { get; } = Currency.Create(
        Name.Create("Canadian dollar"),
        Symbol.Create("C$"),
        ISO4217Code.Create("CAD"),
        false,
        new CurrencyId(Guid.Parse("F7E66C98-4DC8-43D8-B77B-8F9848A3888F"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency CHF { get; } = Currency.Create(
        Name.Create("Swiss franc"),
        Symbol.Create("Fr"),
        ISO4217Code.Create("CHF"),
        false,
        new CurrencyId(Guid.Parse("A418CCB9-82AA-4E69-963B-4736FF1E815A"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency CNY { get; } = Currency.Create(
        Name.Create("Chinese yuan"),
        Symbol.Create("¥"),
        ISO4217Code.Create("CNY"),
        false,
        new CurrencyId(Guid.Parse("9490768D-A205-4DCC-BB66-6A007F661B98"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency SEK { get; } = Currency.Create(
        Name.Create("Swedish krona"),
        Symbol.Create("kr"),
        ISO4217Code.Create("SEK"),
        false,
        new CurrencyId(Guid.Parse("9C32A5F5-DC05-4E83-AB12-D26BDD8D0663"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency NZD { get; } = Currency.Create(
        Name.Create("New Zealand dollar"),
        Symbol.Create("NZ$"),
        ISO4217Code.Create("NZD"),
        false,
        new CurrencyId(Guid.Parse("2BC7BBBC-881B-4460-A099-1D082D11B78B"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency MXN { get; } = Currency.Create(
        Name.Create("Mexican peso"),
        Symbol.Create("$"),
        ISO4217Code.Create("MXN"),
        false,
        new CurrencyId(Guid.Parse("06FC7D4A-AC95-40E9-A685-0434FDA14085"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency NOK { get; } = Currency.Create(
        Name.Create("Norwegian krone"),
        Symbol.Create("kr"),
        ISO4217Code.Create("NOK"),
        false,
        new CurrencyId(Guid.Parse("DD1E997A-BEF8-4F62-86EA-6F45BEDCCCB0"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency SGD { get; } = Currency.Create(
        Name.Create("Singapore dollar"),
        Symbol.Create("S$"),
        ISO4217Code.Create("SGD"),
        false,
        new CurrencyId(Guid.Parse("D9B9B44C-1731-4496-8AEC-1EB6F32689F0"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency HKD { get; } = Currency.Create(
        Name.Create("Hong Kong dollar"),
        Symbol.Create("HK$"),
        ISO4217Code.Create("HKD"),
        false,
        new CurrencyId(Guid.Parse("D502FD4E-77CC-4749-B8B0-CCA05F6C7D1C"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency KRW { get; } = Currency.Create(
        Name.Create("South Korean won"),
        Symbol.Create("₩"),
        ISO4217Code.Create("KRW"),
        false,
        new CurrencyId(Guid.Parse("A353E300-89D7-47B0-9C55-F4C73AD12D98"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency TRY { get; } = Currency.Create(
        Name.Create("Turkish lira"),
        Symbol.Create("₺"),
        ISO4217Code.Create("TRY"),
        false,
        new CurrencyId(Guid.Parse("F1B638FD-F37F-47C0-943E-A35F44EDC2CC"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency INR { get; } = Currency.Create(
        Name.Create("Indian rupee"),
        Symbol.Create("₹"),
        ISO4217Code.Create("INR"),
        false,
        new CurrencyId(Guid.Parse("9D2C8E61-F2BE-43AF-8939-8A0700EA7BDA"))
    );
    
    // ReSharper disable once InconsistentNaming
    public static Currency BRL { get; } = Currency.Create(
        Name.Create("Brazilian real"),
        Symbol.Create("R$"),
        ISO4217Code.Create("BRL"),
        false,
        new CurrencyId(Guid.Parse("0CF996FA-3F23-4209-9BAD-367D21A1C79A"))
    );

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

        builder.Property(currency => currency.IsDefault);

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Currency> GetSeedData()
    {
        yield return PLN;
        yield return GBP;
        yield return EUR;
        yield return USD;
        yield return JPY;
        yield return AUD;
        yield return CAD;
        yield return CHF;
        yield return CNY;
        yield return SEK;
        yield return NZD;
        yield return MXN;
        yield return NOK;
        yield return SGD;
        yield return HKD;
        yield return KRW;
        yield return TRY;
        yield return INR;
        yield return BRL;
    }
}