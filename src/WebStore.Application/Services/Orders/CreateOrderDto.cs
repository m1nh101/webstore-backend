using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Enums;

namespace WebStore.Application.Services.Orders
{
    public class CreateOrderDto
    {
        public decimal Total { get; set; }
        public OrderFlag Status { get; set; }
        public string UserName { get; set; }
    }
}
