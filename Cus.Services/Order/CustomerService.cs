using Cus.Business.Order;
using Cus.DataAccess.Order;
using Cus.Interfaces.Order;
using Cus.Models.Entities;
using Cus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cus.Services.Order
{
    public class CustomerService
    {
        private ICustomer _Customer = null;
        public CustomerService(ICustomer Customer)
        {
            _Customer = Customer;
        }

        public IEnumerable<CusOrders> GetByCusID(string CusID)
        {
            return _Customer.GetByCusID(CusID);
        }

        public IEnumerable<Customers> GetCustomerList()
        {
            return _Customer.GetCustomerList();
        }

        public object GetCustomerByCustomerID(string CustomerId)
        {
            return _Customer.GetCustomerByCustomerID(CustomerId);
        }
    }
}
