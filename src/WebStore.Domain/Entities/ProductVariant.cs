using WebStore.Domain.Common;
using WebStore.Domain.Enums;

namespace WebStore.Domain.Entities;

public class ProductVariant : AuditEntity
{
  public decimal ImportPrice { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public short Size { get; set; }
  public string Description { get; set; } = string.Empty;

  public ProductFlag Status { get; set; } = ProductFlag.InStock;

  public int ProductId { get; set; }
  public virtual Product Product { get; private set; } = null!;

  public int? SaleId { get; set; }
  public virtual Sale? Sale { get; private set; }

  public virtual ICollection<Image> Images { get; private set; } = new List<Image>();

  public virtual ICollection<OrderDetail> OrderItems { get; private set; } = new List<OrderDetail>();

  public virtual ICollection<BillDetail> BillItems { get; private set; } = new List<BillDetail>();
}