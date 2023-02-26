using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database.Configs;

public sealed class ProductVariantEntityConfiguration : BaseEntityConfig<ProductVariant>
{
  public override void Configure(EntityTypeBuilder<ProductVariant> builder)
  {
    base.Configure(builder);

    builder.ToTable("ProductVariants");

    builder.Property(e => e.ImportPrice)
      .HasColumnType("money");

    builder.Property(e => e.Price)
      .HasColumnType("money");
    
    builder.Property(e => e.Size)
      .HasColumnType("smallint");

    builder.Property(e => e.Description)
      .HasColumnType("nvarchar");

    builder.HasOne(e => e.Sale)
      .WithMany(e => e.ProductVariants)
      .HasForeignKey(e => e.SaleId)
      .IsRequired(false);

    builder.HasOne(e => e.Product)
      .WithMany(e => e.ProductVariants)
      .HasForeignKey(e => e.ProductId);
  }
}