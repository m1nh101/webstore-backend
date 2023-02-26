using WebStore.Domain.Common;

namespace WebStore.Domain.Entities;

public class Product : AuditEntity
{
  public string Name { get; set; } = string.Empty;
  public string BranchName { get; set; } = string.Empty;
  
  public virtual IList<ProductVariant> ProductVariants { get; private set; } = new List<ProductVariant>();
}