using Cus.Business.Order;
using Cus.Models.Entities;
using Cus.Services.Order;
using Cus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UseSQLMvc4Application1.Controllers
{
    public class CusController : Controller
    {
        //
        // GET: /Cus/
        //CustomerService context = new CustomerService(new BizCustomerOrder());
        //ProductService productContext = new ProductService(new BizCustomerOrder());
        //EmployeeService employeeContext = new EmployeeService(new BizCustomerOrder());
        //ShipperService shipperContext = new ShipperService(new BizCustomerOrder());
        //OrderService orderContext = new OrderService(new BizCustomerOrder());

        CustomerService context = new CustomerService(new EdmCustomerOrder());
        ProductService productContext = new ProductService(new EdmCustomerOrder());
        EmployeeService employeeContext = new EmployeeService(new EdmCustomerOrder());
        ShipperService shipperContext = new ShipperService(new EdmCustomerOrder());
        OrderService orderContext = new OrderService(new EdmCustomerOrder());

        #region CustomerDropDownList
        private MultiSelectList GetCustomerDropDown(string SelectedValue)
        {
            return new MultiSelectList(
                context.GetCustomerList(), 
                "CustomerID",
                "ContactName", new string[] { SelectedValue });
        }
        #endregion

        public ActionResult Index()
        {
            QueryViewModel query = new QueryViewModel();
            query.CUS_Ordes = context.GetByCusID("ALFKI");
            //保存下拉清單資料
            ViewBag.CustomerLists = GetCustomerDropDown("ALFKI");
            return View(query);
        }

        [HttpPost]
        public ActionResult Index(QueryViewModel param)
        {
            string _param = param.QUERY_PARAM.CustomerID;
            QueryViewModel query = new QueryViewModel();
            query.CUS_Ordes = context.GetByCusID(_param);
            //保存下拉清單資料
            ViewBag.CustomerLists = GetCustomerDropDown(_param);
            return View(query);
        }

        public ActionResult AddOrder()
        {
            AddOrderViewModel addOrder = GetAddOrderViewModel();
            return View(addOrder);
        }

        private AddOrderViewModel GetAddOrderViewModel()
        {
            AddOrderViewModel addOrder = new AddOrderViewModel()
            {
                //CustomerID = "ALFKI",
                //ContactName = context.GetCustomerByCustomerID("ALFKI").ToString(),
                CustomerList = context.GetCustomerList(),
                OrderDate = DateTime.Now,
                ProductList = productContext.GetProducts(),
                EmployeeList = employeeContext.GetEmployees(),
                ShipperList = shipperContext.GetShippers(),
                Quantity = 1
            };
            return addOrder;
        }

        [HttpPost]
        public ActionResult AddOrder(AddOrderViewModel OrderViewModel)
        {
            Orders order = new Orders()
            {
                CustomerID = OrderViewModel.CustomerID,
                EmployeeID = OrderViewModel.EmployeeID,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now.AddDays(7),
                ShippedDate = DateTime.Now.AddDays(2),
                Freight = 20,
                ShipName = shipperContext.GetShippers().Where(c => c.ShipperID == OrderViewModel.ShipperID).FirstOrDefault().CompanyName,
                ORDER_DETAILS = new List<Order_Details>(new Order_Details[] {
                    new Order_Details() {
                        ProductID = OrderViewModel.ProductID,
                        Quantity = OrderViewModel.Quantity,
                        UnitPrice = decimal.Parse(productContext.GetProductPriceByProductID(OrderViewModel.ProductID).ToString()),
                        Discount = 1
                    }
                })
            };
            int result = orderContext.AddOrder(order);
            if (result > 0)
                return RedirectToAction("Index");
            else
                return View();
        }
    }
}
