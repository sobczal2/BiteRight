using BiteRight.Domain.Categories;
using BiteRight.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Name = BiteRight.Domain.Products.Name;

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

        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder
                .Property(p => p.Value)
                .HasColumnName("price_value");

            priceBuilder
                .Property(p => p.CurrencyId)
                .HasConversion(
                    currencyId => currencyId.Value,
                    value => value
                )
                .HasColumnName("price_currency_id");
        });

        builder.OwnsOne(product => product.ExpirationDate, expirationDateBuilder =>
        {
            expirationDateBuilder
                .Property(expirationDate => expirationDate.Value)
                .HasColumnName("expiration_date_value");

            expirationDateBuilder
                .Property(expirationDate => expirationDate.Kind)
                .HasColumnName("expiration_date_kind");
        });

        builder.Property(p => p.CategoryId)
            .HasConversion(
                categoryId => categoryId.Value,
                value => value
            );

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.AddedDateTime)
            .HasConversion(
                addedDateTime => addedDateTime.Value,
                value => value.ToUniversalTime()
            );

        builder.Property(p => p.Usage)
            .HasConversion(
                usage => usage.Amount,
                amount => Usage.CreateSkipValidation(amount)
            );

        builder.Property(p => p.UserId)
            .HasConversion(
                userId => userId.Value,
                value => value
            );

        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(p => p.DisposedState, disposedStateBuilder =>
        {
            disposedStateBuilder
                .Property(disposedState => disposedState.Disposed)
                .HasColumnName("disposed_state_disposed");

            disposedStateBuilder
                .Property(disposedState => disposedState.DisposedDate)
                .HasColumnName("disposed_state_disposed_date");
        });
    }
}