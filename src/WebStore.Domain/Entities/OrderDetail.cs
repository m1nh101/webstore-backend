using System.Diagnostics;
using WebStore.Domain.Common;

namespace WebStore.Domain.Entities;

public class OrderDetail : AuditEntity
{
    public int Quantity { get; set; }
    public decimal Total { get; set; }

    public int OrderId { get; set; }
    public virtual Order Order { get; private set; } = null!;

    public int ProductVariantId { get; set; }
    public virtual ProductVariant ProductVariant { get; private set; } = null!;
}