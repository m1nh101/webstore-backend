using WebStore.Domain.Common;
using WebStore.Domain.Enums;

namespace WebStore.Domain.Entities;

public class Bill : AuditEntity
{
  public decimal TotalPrice { get; set; }
  public decimal ShippingFee { get; set; }
  public string Note { get; set; } = string.Empty;
  public string Division { get; set; } = string.Empty;
  public string ZipCode { get; set; } = string.Empty;
  public string Address1 { get; set; } = string.Empty;
  public string? Address2 { get; set; } = string.Empty;
  public DateTime SuccessDate { get; set; }
  public DateTime CancelDate { get; set; }
  public BillFlag Status { get; set; }
  
  public string CustomerId { get; set; } = string.Empty;
  public virtual User Customer { get; private set; } = null!;

  public virtual IList<BillDetail> Items { get; private set; } = new List<BillDetail>();
}