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
        builder.ToTable("users", "user");
        builder.Ignore(user => user.DomainEvents);
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();
        builder.Property(user => user.IdentityId)
            .HasConversion(
                identityId => identityId.Value,
                value => value
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