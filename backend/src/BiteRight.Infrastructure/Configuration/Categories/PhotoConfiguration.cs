using System;
using System.Collections.Generic;
using BiteRight.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration.Categories;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public static Photo DefaultPhoto => Photo.Default;

    public static Photo DairyPhoto { get; } = Photo.Create(
        new PhotoId(Guid.Parse("98EB4DC2-11B5-440B-BFC1-742FDA8279B7")),
        "dairy.webp"
    );

    public static Photo FruitPhoto { get; } = Photo.Create(
        new PhotoId(Guid.Parse("5E4D81DA-841B-493A-A47B-9F69791E1063")),
        "fruit.webp"
    );

    public static Photo VegetablePhoto { get; } = Photo.Create(
        new PhotoId(Guid.Parse("2EAEE2AC-3EBF-49F2-807B-1B0509F528BA")),
        "vegetable.webp"
    );

    public static Photo MeatPhoto { get; } = Photo.Create(
        new PhotoId(Guid.Parse("4D4C96BC-6990-4B94-982E-D5E7860019A1")),
        "meat.webp"
    );

    public static Photo FishPhoto { get; } = Photo.Create(
        new PhotoId(Guid.Parse("2BFD1C0C-8882-44FA-B73D-8588AD8EC50B")),
        "fish.webp"
    );

    public void Configure(
        EntityTypeBuilder<Photo> builder
    )
    {
        builder.ToTable("photos", "category");
        builder.HasKey(categoryPhoto => categoryPhoto.Id);
        builder.Property(categoryPhoto => categoryPhoto.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Photo> GetSeedData()
    {
        yield return DefaultPhoto;
        yield return DairyPhoto;
        yield return FruitPhoto;
        yield return VegetablePhoto;
        yield return MeatPhoto;
        yield return FishPhoto;
    }
}