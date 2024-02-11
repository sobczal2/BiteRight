using BiteRight.Domain.Categories;
using BiteRight.Domain.Common;
using BiteRight.Domain.Countries;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Products;
using BiteRight.Domain.Units;
using BiteRight.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Translation = BiteRight.Domain.Categories.Translation;

namespace BiteRight.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Profile> Profiles { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<Language> Languages { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Translation> CategoryTranslations { get; set; } = default!;
    public DbSet<Currency> Currencies { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Unit> Units { get; set; } = default!;
    public DbSet<BiteRight.Domain.Units.Translation> UnitTranslations { get; set; } = default!;
    public DbSet<Amount> Amounts { get; set; } = default!;
    public DbSet<Photo> Photos { get; set; } = default!;
    private readonly IDomainEventPublisher _domainEventPublisher;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IDomainEventPublisher domainEventPublisher
    )
        : base(options)
    {
        _domainEventPublisher = domainEventPublisher;
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder
    )
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
    )
    {
        var domainEventHolders = ChangeTracker
            .Entries<IDomainEventHolder>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Count != 0);

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEventHolder in domainEventHolders)
        {
            foreach (var domainEvent in domainEventHolder.DomainEvents)
                await _domainEventPublisher.PublishAsync(domainEvent, cancellationToken);

            domainEventHolder.ClearDomainEvents();
        }

        return result;
    }
}