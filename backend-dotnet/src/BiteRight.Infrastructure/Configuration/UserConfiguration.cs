using BiteRight.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(
        EntityTypeBuilder<User> builder
    )
    {
        builder.ToTable("Users", "User");
        builder.Ignore(user => user.DomainEvents);
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value)
            )
            .ValueGeneratedNever();
        builder.Property(user => user.IdentityId)
            .HasConversion(
                id => id.Value,
                value => IdentityId.Create(value)
            );
        builder.HasIndex(user => user.IdentityId)
            .IsUnique();
        builder.Property(user => user.Username)
            .HasConversion(
                username => username.Value,
                value => Username.CreateSkipValidation(value)
            );
        builder.Property(user => user.Email)
            .HasConversion(
                email => email.Value,
                value => Email.CreateSkipValidation(value)
            );
    }
}