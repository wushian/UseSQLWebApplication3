using Cus.Models.Entities;
using Cus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cus.Interfaces.Order
{
    public interface ICustomer
    {
        IEnumerable<CusOrders> GetByCusID(string CusID);
        IEnumerable<Customers> GetCustomerList();
        object GetCustomerByCustomerID(string CustomerId);
    }
}
