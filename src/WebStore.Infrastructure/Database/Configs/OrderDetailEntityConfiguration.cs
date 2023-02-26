using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database.Configs;

public sealed class OrderDetailEntityConfiguration : BaseEntityConfig<OrderDetail>
{
  public override void Configure(EntityTypeBuilder<OrderDetail> builder)
  {
    base.Configure(builder);

    builder.ToTable("OrderItems");

    builder.Property(e => e.Total)
      .HasColumnType("money");

    builder.HasOne(e => e.ProductVariant)
      .WithMany(e => e.OrderItems)
      .HasForeignKey(e => e.ProductVariantId);
  }
}