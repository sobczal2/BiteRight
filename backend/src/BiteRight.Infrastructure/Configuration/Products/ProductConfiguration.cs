// # ==============================================================================
// # Solution: BiteRight
// # File: ProductConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Name = BiteRight.Domain.Products.Name;

#endregion

namespace BiteRight.Infrastructure.Configuration.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(
        EntityTypeBuilder<Product> builder
    )
    {
        builder.ToTable("products", "product");
        builder.Ignore(product => product.DomainEvents);
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value)
            )
            .ValueGeneratedNever();

        builder.Property(product => product.Name)
            .HasConversion(
                name => name.Value,
                value => Name.CreateSkipValidation(value)
            );

        builder.Property(product => product.Description)
            .HasConversion(
                description => description.Value,
                value => Description.CreateSkipValidation(value)
            );

        builder.OwnsOne(product => product.ExpirationDate, expirationDateBuilder =>
        {
            expirationDateBuilder
                .Property(expirationDate => expirationDate.Value)
                .HasColumnName("expiration_date_value");

            expirationDateBuilder
                .Property(expirationDate => expirationDate.Kind)
                .HasColumnName("expiration_date_kind");
        });

        builder.Property(product => product.CategoryId)
            .HasConversion(
                categoryId => categoryId.Value,
                value => value
            );

        builder.HasOne(product => product.Category)
            .WithMany()
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(product => product.AddedDateTime)
            .HasConversion(
                addedDateTime => addedDateTime.Value,
                value => value.ToUniversalTime()
            );

        builder.Property(product => product.CreatedById)
            .HasConversion(
                userId => userId.Value,
                value => value
            );

        builder.HasOne(product => product.CreatedBy)
            .WithMany()
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(product => product.DisposedState, disposedStateBuilder =>
        {
            disposedStateBuilder
                .Property(disposedState => disposedState.Value)
                .HasColumnName("disposed_state_disposed");

            disposedStateBuilder
                .Property(disposedState => disposedState.DateTime)
                .HasColumnName("disposed_state_disposed_date");
        });
    }
}