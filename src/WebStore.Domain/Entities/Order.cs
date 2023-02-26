using WebStore.Domain.Common;
using WebStore.Domain.Enums;

namespace WebStore.Domain.Entities;

public class Order : AuditEntity
{
  public OrderFlag Status { get; set; }

  public string UserId { get; private set; } = string.Empty;
  public virtual User User { get; private set; } = null!;

  public virtual IList<OrderDetail> Items { get; private set; } = new List<OrderDetail>();
}