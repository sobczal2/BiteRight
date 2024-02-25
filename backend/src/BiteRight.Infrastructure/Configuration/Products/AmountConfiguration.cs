// # ==============================================================================
// # Solution: BiteRight
// # File: AmountConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Products;
using BiteRight.Domain.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace BiteRight.Infrastructure.Configuration.Products;

public class AmountConfiguration : IEntityTypeConfiguration<Amount>
{
    public void Configure(
        EntityTypeBuilder<Amount> builder
    )
    {
        builder.ToTable("amounts", "product");
        builder.HasKey(amount => amount.Id);
        builder.Property(amount => amount.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();

        builder.Property(amount => amount.CurrentValue);

        builder.Property(amount => amount.MaxValue);

        builder.Property(amount => amount.UnitId)
            .HasConversion(
                unitId => unitId.Value,
                value => value
            );

        builder.HasOne(amount => amount.Unit)
            .WithMany()
            .HasForeignKey(amount => amount.UnitId);
        
        builder.Property(amount => amount.ProductId)
            .HasConversion(
                productId => productId.Value,
                value => value
            );

        builder.HasOne(amount => amount.Product)
            .WithOne(product => product.Amount)
            .HasForeignKey<Amount>(price => price.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}