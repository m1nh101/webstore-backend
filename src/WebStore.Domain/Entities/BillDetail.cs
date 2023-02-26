using WebStore.Domain.Common;

namespace WebStore.Domain.Entities;

public class BillDetail : AuditEntity
{
  public int Quantity { get; set; }
  public decimal TaxRate { get; set; }
  public decimal TotalMoney { get; set; }
  public string Note { get; set; } = string.Empty;

  public int ProductVariantId { get; set; }
  public virtual ProductVariant ProductVariant { get; private set; } = null!;

  public int BillId { get; set; }
  public virtual Bill Bill { get; private set; } = null!;
}