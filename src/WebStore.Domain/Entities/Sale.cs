using WebStore.Domain.Common;

namespace WebStore.Domain.Entities;

public class Sale : AuditEntity
{
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public short Value { get; set; }

  public bool IsInprocess()
  {
    var now = DateTime.Now;

    return now >= StartDate && now <= EndDate;
  }

  public virtual IList<ProductVariant> ProductVariants { get; private set; } = new List<ProductVariant>();
}