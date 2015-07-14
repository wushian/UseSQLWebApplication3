using Cus.EDMModel;
using Cus.Interfaces.Order;
using Cus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cus.Business.Order
{
    public class EdmCustomerOrder : ICustomer, IOrder, IProduct, IEmployee, IShipper
    {
        private readonly NORTHWNDEntities _context = new NORTHWNDEntities();
        public IEnumerable<ViewModels.CusOrders> GetByCusID(string CusID)
        {
            var result = from order in _context.Orders
                         join order_d in _context.Order_Details
                             on order.OrderID equals order_d.OrderID
                         join product in _context.Products
                             on order_d.ProductID equals product.ProductID
                         join cus in _context.Customers
                             on order.CustomerID equals cus.CustomerID
                         where cus.CustomerID == CusID
                         select new CusOrders()
                         {
                             CustomerID = cus.CustomerID,
                             CompanyName = cus.CompanyName,
                             ContactName = cus.ContactName,
                             City = cus.City,
                             OrderID = order.OrderID,
                             UnitPrice = order_d.UnitPrice,
                             ProductID = product.ProductID,
                             ProductName = product.ProductName,
                             OrderDate = order.OrderDate.Value
                         };

            return result.ToList();
        }

        public IEnumerable<Models.Entities.Customers> GetCustomerList()
        {
            var result = from cus in _context.Customers
                         select new Models.Entities.Customers()
                         {
                             CustomerID = cus.CustomerID,
                             ContactName = cus.ContactName,
                             ContactTitle = cus.ContactTitle,
                             CompanyName = cus.CompanyName,
                             Address = cus.Address,
                             City = cus.City,
                             Country = cus.Country,
                             Fax = cus.Fax,
                             Phone = cus.Phone,
                             PostalCode = cus.PostalCode,
                             Region = cus.Region
                         };

            return result.ToList();
        }

        public object GetCustomerByCustomerID(string CustomerId)
        {
            var result = (from cus in _context.Customers
                         where cus.CustomerID == CustomerId
                         select cus).FirstOrDefault();
            if (result != null)
                return result.ContactName;
            else
                return string.Empty;
        }

        public int AddOrder(Models.Entities.Orders orders)
        {
            Orders order = new Orders()
            {
                CustomerID = orders.CustomerID,
                EmployeeID = orders.EmployeeID,
                OrderDate = orders.OrderDate,
                RequiredDate = orders.RequiredDate,
                ShippedDate = orders.ShippedDate,
                Freight = orders.Freight,
                ShipName = orders.ShipName,
                Order_Details = (from c in orders.ORDER_DETAILS
                                select new Order_Details()
                                {
                                    ProductID = c.ProductID,
                                    Quantity = c.Quantity,
                                    UnitPrice = decimal.Parse(GetProductPriceByProductID(c.ProductID).ToString()),
                                    Discount = 1
                                }).ToList()
            };
            _context.Orders.Add(order);
            return _context.SaveChanges();
        }

        public object GetProductPriceByProductID(int ProductID)
        {
            var result = (from product in _context.Products
                         where product.ProductID == ProductID
                         select product).FirstOrDefault();

            if (result != null)
                return result.UnitPrice;
            else
                return 0;
        }

        public IEnumerable<Models.Entities.Products> GetProducts()
        {
            var result = from product in _context.Products
                         select new Models.Entities.Products()
                         {
                             ProductID = product.ProductID,
                             ProductName = product.ProductName,
                             CategoryID = product.CategoryID.HasValue?product.CategoryID.Value:0,
                             QuantityPerUnit = product.QuantityPerUnit,
                             ReorderLevel = product.ReorderLevel.HasValue?product.ReorderLevel.Value:(short)0,
                             SupplierID = product.SupplierID.HasValue?product.SupplierID.Value:0,
                             UnitPrice = product.UnitPrice.HasValue?product.UnitPrice.Value:0,
                             UnitsInStock = product.UnitsInStock.HasValue?product.UnitsInStock.Value:(short)0,
                             UnitsOnOrder = product.UnitsOnOrder.HasValue?product.UnitsOnOrder.Value:(short)0
                         };

            return result.ToList();
        }

        public IEnumerable<Models.Entities.Employees> GetEmployees()
        {
            var result = from emp in _context.Employees
                         select new Models.Entities.Employees()
                         {
                             EmployeeID = emp.EmployeeID,
                             FirstName = emp.FirstName,
                             LastName = emp.LastName,
                             Address = emp.Address,
                             City = emp.City,
                             Country = emp.Country,
                             BirthDate = emp.BirthDate,
                             Extension = emp.Extension,
                             HireDate = emp.HireDate,
                             HomePhone = emp.HomePhone,
                             Notes = emp.Notes,
                             PhotoPath = emp.PhotoPath,
                             Region = emp.Region,
                             ReportsTo = emp.ReportsTo.HasValue?emp.ReportsTo.Value:0,
                             Title = emp.Title,
                             TitleOfCourtesy = emp.TitleOfCourtesy
                         };

            return result.ToList();
        }

        public IEnumerable<Models.Entities.Shippers> GetShippers()
        {
            var result = from ship in _context.Shippers
                         select new Models.Entities.Shippers()
                         {
                             ShipperID = ship.ShipperID,
                             CompanyName = ship.CompanyName,
                             Phone = ship.Phone
                         };

            return result.ToList();
        }
    }
}
