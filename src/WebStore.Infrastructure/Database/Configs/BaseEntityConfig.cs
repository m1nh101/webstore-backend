using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Common;

namespace WebStore.Infrastructure.Database.Configs;

public abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
  where TEntity : AuditEntity
{
  public virtual void Configure(EntityTypeBuilder<TEntity> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
  }
}
