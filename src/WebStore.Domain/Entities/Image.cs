using WebStore.Domain.Common;

namespace WebStore.Domain.Entities;

public class Image : AuditEntity
{
  public string Url { get; set; } = string.Empty;
  public int ProductVariantId { get; set; }
  public virtual ProductVariant ProductVariant { get; private set; } = null!;
}