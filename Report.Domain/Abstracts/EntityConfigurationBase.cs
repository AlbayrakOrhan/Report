using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Report.Domain.Abstracts;

public abstract class EntityConfigurationBase<T> where T : EntityBase
{
    protected abstract void Configure(EntityTypeBuilder<T> eb);

    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<T>(eb =>
        {
            eb.HasKey(b => b.Id);
            eb.Property(b => b.CreatedDate);
            eb.Property(b => b.ModifiedDate);
            Configure(eb);
        });
    }
}