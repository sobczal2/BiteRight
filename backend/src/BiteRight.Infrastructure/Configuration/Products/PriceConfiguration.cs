// # ==============================================================================
// # Solution: BiteRight
// # File: PriceConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 25-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace BiteRight.Infrastructure.Configuration.Products;

public class PriceConfiguration : IEntityTypeConfiguration<Price>
{
    public void Configure(
        EntityTypeBuilder<Price> builder
    )
    {
        builder.ToTable("prices", "product");
        builder.HasKey(price => price.Id);
        builder.Property(price => price.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();

        builder.Property(price => price.Value);

        builder.Property(price => price.CurrencyId)
            .HasConversion(
                currencyId => currencyId.Value,
                value => value
            );

        builder.HasOne(price => price.Currency)
            .WithMany()
            .HasForeignKey(price => price.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(price => price.ProductId)
            .HasConversion(
                productId => productId.Value,
                value => value
            );

        builder.HasOne(price => price.Product)
            .WithOne(product => product.Price)
            .HasForeignKey<Price>(price => price.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}