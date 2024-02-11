using BiteRight.Domain.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration.Units;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public static Unit Liter { get; } = Unit.Create(
        UnitSystem.Metric,
        new UnitId(new Guid("B6D4D4DD-C035-4047-B8EE-48937CB1F368"))
    );

    public static Unit Kilogram { get; } = Unit.Create(
        UnitSystem.Metric,
        new UnitId(new Guid("CDE52E6C-5D9D-4876-978A-BF67C02CC8BE"))
    );

    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.ToTable("units", "unit");
        builder.Ignore(unit => unit.DomainEvents);
        builder.HasKey(unit => unit.Id);
        builder.Property(unit => unit.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Unit> GetSeedData()
    {
        yield return Liter;
        yield return Kilogram;
    }
}