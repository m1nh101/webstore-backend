using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database.Configs;

public sealed class ImageEntityConfiguration : BaseEntityConfig<Image>
{
  public override void Configure(EntityTypeBuilder<Image> builder)
  {
    base.Configure(builder);

    builder.ToTable("Images");

    builder.Property(e => e.Url)
      .HasColumnType("varchar")
      .IsRequired();

    builder.HasOne(e => e.ProductVariant)
      .WithMany(e => e.Images)
      .HasForeignKey(e => e.ProductVariantId);
  }
}