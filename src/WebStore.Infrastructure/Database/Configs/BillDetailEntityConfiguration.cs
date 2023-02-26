using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database.Configs;

public sealed class BillDetailEntityConfiguration : BaseEntityConfig<BillDetail>
{
  public override void Configure(EntityTypeBuilder<BillDetail> builder)
  {
    base.Configure(builder);

    builder.ToTable("BillItems");

    builder.Property(e => e.TaxRate)
      .HasColumnType("decimal(4,2)");

    builder.Property(e => e.TotalMoney)
      .HasColumnType("money");

    builder.Property(e => e.Note)
      .IsRequired(false);

    builder.HasOne(e => e.Bill)
      .WithMany(e => e.Items)
      .HasForeignKey(e => e.BillId);

    builder.HasOne(e => e.ProductVariant)
      .WithMany(e => e.BillItems)
      .HasForeignKey(e => e.ProductVariantId);
  }
}