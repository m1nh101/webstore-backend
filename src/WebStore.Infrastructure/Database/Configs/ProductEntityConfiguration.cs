using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database.Configs;

public sealed class ProductEntityConfiguration : BaseEntityConfig<Product>
{
  public override void Configure(EntityTypeBuilder<Product> builder)
  {
    base.Configure(builder);

    builder.ToTable("Products");

    builder.Property(e => e.Name)
      .HasColumnType("nvarchar(255)")
      .HasMaxLength(255)
      .IsRequired();

    builder.Property(e => e.BranchName)
      .HasColumnType("nvarchar(255)")
      .HasMaxLength(255)
      .IsRequired();
  }
}