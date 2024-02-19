// # ==============================================================================
// # Solution: BiteRight
// # File: CategoryConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using BiteRight.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace BiteRight.Infrastructure.Configuration.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public static Category None { get; } = Category.Create(
        null,
        true,
        new CategoryId(Guid.Parse("C82E0550-26CF-410D-8CEC-5CF62BADA757"))
    );
    
    public static Category Dairy { get; } = Category.Create(
        PhotoConfiguration.DairyPhoto.Id,
        false,
        new CategoryId(Guid.Parse("E8C78317-70AC-4051-805E-ECE2BB37656F"))
    );

    public static Category Fruit { get; } = Category.Create(
        PhotoConfiguration.FruitPhoto.Id,
        false,
        new CategoryId(Guid.Parse("1FD7ED59-9E34-40AB-A03D-6282B5D9FD86"))
    );

    public static Category Vegetable { get; } = Category.Create(
        PhotoConfiguration.VegetablePhoto.Id,
        false,
        new CategoryId(Guid.Parse("349774C7-3249-4245-A1E2-5B70C5725BBF"))
    );

    public static Category Meat { get; } = Category.Create(
        PhotoConfiguration.MeatPhoto.Id,
        false,
        new CategoryId(Guid.Parse("5E40BA93-D28C-4CF3-9E75-379040A18E52"))
    );

    public static Category Fish { get; } = Category.Create(
        PhotoConfiguration.FishPhoto.Id,
        false,
        new CategoryId(Guid.Parse("17C56168-C9EC-4FFB-A074-495A02AB0359"))
    );
    
    public static Category Beverage { get; } = Category.Create(
        PhotoConfiguration.BeveragePhoto.Id,
        false,
        new CategoryId(Guid.Parse("7289CBC9-8249-4FC1-B2D3-BAC90AD32595"))
    );
    
    public static Category Snack { get; } = Category.Create(
        PhotoConfiguration.SnackPhoto.Id,
        false,
        new CategoryId(Guid.Parse("E86CAF03-EA3B-49AB-B499-68E387919FB6"))
    );
    
    public static Category Wheat { get; } = Category.Create(
        PhotoConfiguration.WheatPhoto.Id,
        false,
        new CategoryId(Guid.Parse("BF69966B-0CBC-4F5D-9388-C05926775CBF"))
    );

    public void Configure(
        EntityTypeBuilder<Category> builder
    )
    {
        builder.ToTable("categories", "category");
        builder.Ignore(category => category.DomainEvents);
        builder.HasKey(category => category.Id);
        builder.Property(category => category.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();

        builder.Property(category => category.PhotoId)
            .HasConversion(
                photoId => photoId!.Value,
                value => value
            );

        builder.HasOne(category => category.Photo)
            .WithOne()
            .HasForeignKey<Category>(category => category.PhotoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(category => category.IsDefault);

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Category> GetSeedData()
    {
        yield return None;
        yield return Dairy;
        yield return Fruit;
        yield return Vegetable;
        yield return Meat;
        yield return Fish;
        yield return Beverage;
        yield return Snack;
        yield return Wheat;
    }
}