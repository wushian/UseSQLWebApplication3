using Cus.Interfaces.Order;
using Cus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cus.Services.Order
{
    public class OrderService
    {
        private IOrder _Order = null;
        public OrderService(IOrder Order)
        {
            _Order = Order;
        }

        public int AddOrder(Orders orders)
        {
            return _Order.AddOrder(orders);
        }
    }
}
