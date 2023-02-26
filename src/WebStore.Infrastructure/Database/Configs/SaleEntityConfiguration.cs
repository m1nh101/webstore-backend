using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database.Configs;

public sealed class SaleEntityConfiguration : BaseEntityConfig<Sale>
{
  public override void Configure(EntityTypeBuilder<Sale> builder)
  {
    base.Configure(builder);

    builder.ToTable("Sales");

    builder.Property(e => e.Value)
      .HasColumnType("smallint");
  }
}