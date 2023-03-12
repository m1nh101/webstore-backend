using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Enums;

namespace WebStore.Application.Services.Orders
{
    public class AddItemToOrder
    {
        public AddItemToOrder(int quantity, decimal totalOrderPrice, decimal totaltemPrice)
        {
            Quantity = quantity;
            TotalOrderPrice = totalOrderPrice;
            TotaltemPrice = totaltemPrice;
        }

        public int Quantity { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public decimal TotaltemPrice { get; set; }
    }
}
