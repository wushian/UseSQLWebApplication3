using Cus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cus.Interfaces.Order
{
    public interface IOrder
    {
        int AddOrder(Orders orders);
    }
}
