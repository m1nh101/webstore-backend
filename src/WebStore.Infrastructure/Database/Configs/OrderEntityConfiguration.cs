using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database.Configs;

public sealed class OrderEntityConfiguration : BaseEntityConfig<Order>
{
  public override void Configure(EntityTypeBuilder<Order> builder)
  {
    base.Configure(builder);

    builder.ToTable("Orders");

    builder.HasOne(e => e.User)
      .WithMany(e => e.Orders)
      .HasForeignKey(e => e.UserId);
  }
}