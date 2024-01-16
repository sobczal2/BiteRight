using BiteRight.Domain.Categories;
using BiteRight.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration.Categories;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
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
    }

    public static Photo DefaultPhoto { get; } = Photo.Default;
}