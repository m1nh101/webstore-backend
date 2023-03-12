using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Application.Services.Orders;
using WebStore.Domain.Entities;

namespace WebStore.Application.Common.Contracts
{
    public interface IOrderService
    {
        Task<AddItemToOrder> AddItemToOrderAsync(int productVariantId);
        Task RemoveItemAsync(int orderId, int itemId);
        Task ReduceNumber(int orderId, int productVariantId);
        Task IncreaseNumber(int id);
        Task CheckOut(string userId);
    }
}
