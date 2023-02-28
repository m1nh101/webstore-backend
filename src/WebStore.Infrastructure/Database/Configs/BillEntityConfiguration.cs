using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database.Configs;

public sealed class BillEntityConfiguration : BaseEntityConfig<Bill>
{
  public override void Configure(EntityTypeBuilder<Bill> builder)
  {
    base.Configure(builder);

    builder.ToTable("Bills");

    builder.Property(e => e.TotalPrice)
      .HasColumnType("money");

    builder.Property(e => e.ShippingFee)
      .HasColumnType("money");

    builder.Property(e => e.Address1)
      .HasColumnType("nvarchar(255)");

    builder.Property(e => e.Address2)
      .HasColumnType("nvarchar(255)")
      .IsRequired(false);

    builder.HasOne(e => e.Customer)
      .WithMany(e => e.Bills)
      .HasForeignKey(e => e.CustomerId);
  }
}