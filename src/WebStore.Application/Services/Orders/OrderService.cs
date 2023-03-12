using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Application.Common.Abstractions;
using WebStore.Application.Common.Contracts;
using WebStore.Domain.Entities;
using WebStore.Domain.Enums;

namespace WebStore.Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IWebStoreContext _webStoreContext;

        private readonly IUserSession _userSession;

        public OrderService(IWebStoreContext webStoreContext, IUserSession userSession)
        {
            _webStoreContext = webStoreContext;
            _userSession = userSession;
        }

        public async Task<AddItemToOrder> AddItemToOrderAsync([NotNull] int productVariantId)
        {
            var variant = await _webStoreContext.GetTable<ProductVariant>().FirstOrDefaultAsync(x => x.Id == productVariantId);
            if (variant == null)
                throw new NullReferenceException(nameof(variant));
            var order = await _webStoreContext.GetTable<Order>().Include(c => c.Items).Include(c => c.User).FirstOrDefaultAsync(c => c.UserId == _userSession.UserId);
            if (order == null)
                order = new Order();
            var item = order.Items?.FirstOrDefault(c => c.ProductVariantId == productVariantId);

            if (order.Id == 0)
            {
                order.Items = new List<OrderDetail>
                {
                    new OrderDetail()
                    {
                        ProductVariantId = variant.Id,
                        Quantity = 1,
                        Total = variant.Price
                    }
                };
                order.Total = variant.Price;
                await _webStoreContext.GetTable<Order>().AddAsync(order);
                await _webStoreContext.Save();
                return new AddItemToOrder(item.Quantity, order.Total, item.Total);
            }
            if (item != null)
            {
                item.Quantity++;
                item.Total = variant.Price * item.Quantity;
                order.Total += variant.Price;
                _webStoreContext.GetTable<Order>().Update(order);
                await _webStoreContext.Save();
                return new AddItemToOrder(item.Quantity, order.Total, item.Total);
            }
            var detail = new OrderDetail
            {
                OrderId = order.Id,
                Total = variant.Price,
                Quantity = 1,
                ProductVariantId = variant.Id
            };
            order.Total += detail.Total;
            _webStoreContext.GetTable<OrderDetail>().Update(detail);
            await _webStoreContext.GetTable<OrderDetail>().AddAsync(detail);
            await _webStoreContext.Save();
            return new AddItemToOrder(detail.Quantity, order.Total, detail.Total);
        }

        public async Task IncreaseNumber(int id)
        {
            var item = await _webStoreContext.GetTable<OrderDetail>().FindAsync(id);

            if (item == null)
                throw new NullReferenceException(nameof(item));

            var order = await _webStoreContext.GetTable<Order>().Include(x => x.Items).FirstOrDefaultAsync(x => x.Status == OrderFlag.Inprocess && x.UserId == _userSession.UserId);

            if (order == null)
                throw new NullReferenceException(nameof(item));

            var variant = await _webStoreContext.GetTable<ProductVariant>().FirstOrDefaultAsync(x => x.Id == item.ProductVariantId);

            if (variant == null)
                throw new NullReferenceException(nameof(item));

            item.Quantity++;

            if (item.Quantity > variant.Quantity)
                throw new ArgumentOutOfRangeException(nameof(item));

            item.Total += variant.Price;
            order.Total += variant.Price;

            _webStoreContext.GetTable<OrderDetail>().Update(item);

            _webStoreContext.GetTable<Order>().Update(order);

            await _webStoreContext.Save();
        }

        public Task ReduceNumber(int orderId, int productVariantId)
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
