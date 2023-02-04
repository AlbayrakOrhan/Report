using MediatR;
using Microsoft.EntityFrameworkCore;
using Report.Domain.Abstracts;
using Report.Domain.Entities;
using Report.Domain.EntityConfigurations;
using Report.Domain.Interfaces;

namespace Report.Persistence.Context;

public class ReportDbContext : DbContext
{
    private readonly IMediator _mediator;

    public ReportDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<ReportRequest> ReportRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ReportRequestConfiguration().Configure(modelBuilder);
    }

    public override int SaveChanges()
    {
        BeforeSave();
        var numberOfChangedStates = base.SaveChanges();
        DispatchDomainEvents().GetAwaiter().GetResult();
        return numberOfChangedStates;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        BeforeSave();
        var numberOfChangedStates = await base.SaveChangesAsync(cancellationToken);
        await DispatchDomainEvents();
        return numberOfChangedStates;
    }

    private void BeforeSave()
    {
        ChangeTracker.DetectChanges();
        var added = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Added)
            .Select(t => t.Entity)
            .ToArray();

        foreach (var entity in added)
        {
            if (entity is EntityBase track)
            {
                track.CreatedDate = DateTime.Now;
            }
        }

        var deleted = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Deleted)
            .ToArray();
        foreach (var item in deleted)
        {
            if (item.Entity is not ISoftDeletableEntity entity)
                continue;

            entity.IsDeleted = true;
            item.State = EntityState.Modified;
        }

        var modified = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified)
            .Select(t => t.Entity)
            .ToArray();

        foreach (var entity in modified)
        {
            if (entity is EntityBase track)
            {
                track.ModifiedDate = DateTime.Now;
            }
        }
    }

    private async Task DispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<EntityBase>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach (var entityDomainEvent in events)
                await _mediator.Publish(entityDomainEvent);
        }
    }
}