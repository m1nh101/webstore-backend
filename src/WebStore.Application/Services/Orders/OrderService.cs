using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Application.Common.Abstractions;
using WebStore.Application.Common.Contracts;
using WebStore.Domain.Entities;

namespace WebStore.Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IWebStoreContext _webStoreContext;
    

        public OrderService(IWebStoreContext webStoreContext)
        {
            _webStoreContext = webStoreContext;
        }

        public async Task<Order> AddOrderAsync(string userId, int productVariantId)
        {
            var product = await _webStoreContext.GetTable<ProductVariant>().FindAsync(productVariantId);
            var order = await _webStoreContext.GetTable<Order>().Include(c=>c.Items).Include(c=>c.User).FirstOrDefaultAsync(c=>c.UserId == userId);
            var item = order.Items?.FirstOrDefault(c=>c.ProductVariantId == productVariantId);
            if(order == null)
            {
                order.CreatedAt = DateTime.Now;
                order.CreatedBy = order.User.UserName;
                order.UserId  = userId;
                order.Items = new List<OrderDetail>
                {
                    new OrderDetail()
                    {
                        ProductVariantId = product.Id,
                        Quantity = 1,
                        Total = product.Price,
                        OrderId = order.Id,
                    }
                };
                order.Total = product.Price;
                await _webStoreContext.GetTable<Order>().AddAsync(order);
                await _webStoreContext.Save();
                return order;
            }
            if (item != null)
            {
                item.Quantity++;
                item.Total = product.Price * product.Quantity;
                order.Total += product.Price;
                _webStoreContext.GetTable<Order>().Update(order);
                await _webStoreContext.Save();
                return order;
            }
             var Detail = new OrderDetail 
             {
                 OrderId = order.Id, 
                 Total = product.Price, 
                 Quantity = 1, 
                 ProductVariantId = product.Id
             };
            order.Total += Detail.Total;
            _webStoreContext.GetTable<OrderDetail>().Update(Detail);
            await _webStoreContext.GetTable<OrderDetail>().AddAsync(Detail);
            await _webStoreContext.Save();
            return order;
        }
        
        public async Task IncreaseQuantity(int orderId, int productVariantId)
        {
            var item = await _webStoreContext.GetTable<OrderDetail>().FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductVariantId == productVariantId);
            if(item == null)
                throw new NullReferenceException(nameof(item));
            var order = await _webStoreContext.GetTable<Order>().Include(x=>x.Items).FirstOrDefaultAsync(x=>x.Id == orderId);
            if(order == null)
                throw new NullReferenceException(nameof(item));
            var variant = await _webStoreContext.GetTable<ProductVariant>().FirstOrDefaultAsync(x=>x.Id == productVariantId);
            if(variant == null)
                throw new NullReferenceException(nameof(item));
            if(item.Quantity == variant.Quantity)
                return;
            item.Quantity++;
            item.Total += variant.Price;
            order.Total += variant.Price;
            _webStoreContext.GetTable<OrderDetail>().Update(item);
            _webStoreContext.GetTable<Order>().Update(order);
            await _webStoreContext.Save();
        }
        public Task ReduceQuantity(int orderId, int productVariantId)
        {
            throw new NotImplementedException();
        }
        public Task RemoveItemAsync(int orderId, int itemId)
        {
            throw new NotImplementedException();
        }
        public Task CheckOut(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
