using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Application.Common.Contracts
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(string userId, int ProductVariantId);
        Task RemoveItemAsync(int orderId, int itemId);
        Task ReduceQuantity(int orderId, int ProductId);
        Task IncreaseQuantity(int orderId, int ProductId);
        Task CheckOut(string userId);
    }
}
