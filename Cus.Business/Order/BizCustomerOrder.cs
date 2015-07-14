using Cus.DataAccess.Order;
using Cus.Interfaces.Order;
using Cus.Models.Entities;
using Cus.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WistronITs.Data.DAL;

namespace Cus.Business.Order
{
    public class BizCustomerOrder: ICustomer, IOrder, IProduct, IEmployee, IShipper
    {
        #region 原始 DAL 實作
        private DalCusOrders _context = null;
        protected DalCusOrders Context
        {
            get
            {
                if(_context==null)
                    _context = new DalCusOrders();
                return _context;
            }
        }
        #endregion

        #region Wistron DAL Framework
        private MSSQLObject _MSSql = null;
        protected MSSQLObject MSSql
        {
            get 
            { 
                if(_MSSql==null)
                    _MSSql = new MSSQLObject(new WistronITs.Data.DAL.DataAccess());
                return _MSSql;
            }
        }
        #endregion

        public BizCustomerOrder() { }
        public IEnumerable<CusOrders> GetByCusID(string CusID)
        {
            return MSSql.GetEnumerableByDataTable<CusOrders>(Context.GetByCusID(CusID));
        }

        public IEnumerable<Customers> GetCustomerList()
        {
            return MSSql.GetEnumerableByDataTable<Customers>(Context.GetCustomerList());
        }

        public object GetCustomerByCustomerID(string CustomerId)
        {
            return Context.GetCustomerByCustomerID(CustomerId);
        }

        public object GetProductPriceByProductID(int ProductID)
        {
            return Context.GetProductPriceByProductID(ProductID);
        }

        public IEnumerable<Products> GetProducts()
        {
            return MSSql.GetEnumerableByDataTable<Products>(Context.GetProducts());
        }

        public int AddOrder(Orders orders)
        {
            return Context.AddOrder(orders);
        }

        public IEnumerable<Employees> GetEmployees()
        {
            return MSSql.GetEnumerableByDataTable<Employees>(Context.GetEmployees());
        }

        public IEnumerable<Shippers> GetShippers()
        {
            return MSSql.GetEnumerableByDataTable<Shippers>(Context.GetShippers());
        }
    }
}
