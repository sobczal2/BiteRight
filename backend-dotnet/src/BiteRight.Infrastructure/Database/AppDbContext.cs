using BiteRight.Domain.Common;
using BiteRight.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Infrastructure.Database;

public class AppDbContext : DbContext
{
    private readonly IDomainEventPublisher _domainEventPublisher;
    public DbSet<User> Users { get; set; } = null!;
    
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
            .Where(x => x.DomainEvents.Any());
        
        var result = await base.SaveChangesAsync(cancellationToken);
        
        foreach (var domainEventHolder in domainEventHolders)
        {
            foreach (var domainEvent in domainEventHolder.DomainEvents)
            {
                await _domainEventPublisher.PublishAsync(domainEvent, cancellationToken);
            }
            domainEventHolder.ClearDomainEvents();
        }
        
        return result;
    }
}