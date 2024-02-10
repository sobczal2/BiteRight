using BiteRight.Domain.Categories;
using BiteRight.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
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

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Category> GetSeedData()
    {
        yield return Dairy;
        yield return Fruit;
        yield return Vegetable;
        yield return Meat;
        yield return Fish;
        yield return None;
    }

    public static Category Dairy { get; } = Category.Create(
        PhotoConfiguration.DairyPhoto.Id,
        new CategoryId(new Guid("E8C78317-70AC-4051-805E-ECE2BB37656F"))
    );

    public static Category Fruit { get; } = Category.Create(
        PhotoConfiguration.FruitPhoto.Id,
        new CategoryId(new Guid("1FD7ED59-9E34-40AB-A03D-6282B5D9FD86"))
    );

    public static Category Vegetable { get; } = Category.Create(
        PhotoConfiguration.VegetablePhoto.Id,
        new CategoryId(new Guid("349774C7-3249-4245-A1E2-5B70C5725BBF"))
    );

    public static Category Meat { get; } = Category.Create(
        PhotoConfiguration.MeatPhoto.Id,
        new CategoryId(new Guid("5E40BA93-D28C-4CF3-9E75-379040A18E52"))
    );

    public static Category Fish { get; } = Category.Create(
        PhotoConfiguration.FishPhoto.Id,
        new CategoryId(new Guid("17C56168-C9EC-4FFB-A074-495A02AB0359"))
    );

    public static Category None { get; } = Category.Create(
        null,
        new CategoryId(new Guid("C82E0550-26CF-410D-8CEC-5CF62BADA757"))
    );
}