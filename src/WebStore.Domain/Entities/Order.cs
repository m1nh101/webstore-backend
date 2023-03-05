using System.Diagnostics.CodeAnalysis;
using WebStore.Domain.Common;
using WebStore.Domain.Enums;

namespace WebStore.Domain.Entities;

public class Order : AuditEntity
{
    public decimal Total { get; set; }
    public OrderFlag Status { get; set; }

    public string UserId { get; set; } = string.Empty;
    public virtual User User { get; private set; } = null!;

    public virtual IList<OrderDetail> Items { get; set; } = new List<OrderDetail>();
    //public Order(decimal total, OrderFlag status, string userId, IList<OrderDetail> items)
    //{
    //    Total = total;
    //    Status = status;
    //    UserId = userId;
    //    Items = items;
    //}
    //public void AddOrderDetail(OrderDetail orderDetail)
    //{
    //    Items.Add(orderDetail);
    //}

}