using Cus.Business.Order;
using Cus.DataAccess.Order;
using Cus.Models.Entities;
using Cus.Services.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UseSQLWebApplication1
{
    public partial class AddOrder : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                GetHttpGetParam();
                CreateList();
            }
        }

        private void GetHttpGetParam()
        {
            if(Request["customerid"]!=null)
            {
                labContactID.Text = Request["customerid"];
                CustomerService cusContext = new CustomerService(new BizCustomerOrder());
                object customerName = cusContext.GetCustomerByCustomerID(labContactID.Text);
                if(customerName!=null)
                {
                    txtContactName.Text = customerName.ToString();
                }
            }
        }

        private void CreateList()
        {
            ProductService productContext = new ProductService(new BizCustomerOrder());
            IEnumerable<Products> productList = productContext.GetProducts();
            ddlProducts.DataSource = productList;
            ddlProducts.DataTextField = "ProductName";
            ddlProducts.DataValueField = "ProductID";
            ddlProducts.DataBind();
            labUnitPrice.Text = productList.FirstOrDefault().UnitPrice.ToString();
            //ddlProducts.Items.Insert(0, new ListItem("(請選擇)", "0"));
            ShipperService shipperrContext = new ShipperService(new BizCustomerOrder());
            ddlShippers.DataSource = shipperrContext.GetShippers();
            ddlShippers.DataTextField = "CompanyName";
            ddlShippers.DataValueField = "ShipperID";
            ddlShippers.DataBind();
            EmployeeService empContext = new EmployeeService(new BizCustomerOrder());
            ddlEmployee.DataSource = empContext.GetEmployees();
            ddlEmployee.DataTextField = "FirstName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
        }

        protected void btnAddOrder_Click(object sender, EventArgs e)
        {
            OrderService orderContext = new OrderService(new BizCustomerOrder());
            //DalCusOrders DalCus = new DalCusOrders();
            Orders order = new Orders()
            {
                CustomerID = labContactID.Text,
                EmployeeID = int.Parse(ddlEmployee.SelectedValue),
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now.AddDays(7),
                ShippedDate = DateTime.Now.AddDays(2),
                Freight = 20,
                ShipName = ddlShippers.SelectedItem.Text,
                ORDER_DETAILS = new List<Order_Details>(new Order_Details[] {
                    new Order_Details() {
                        ProductID = int.Parse(ddlProducts.SelectedValue),
                        Quantity = Int16.Parse(txtQuantity.Text),
                        UnitPrice = decimal.Parse(labUnitPrice.Text),
                        Discount = 1
                    }
                })
            };
            int result = orderContext.AddOrder(order);
            if(result>0)
            {
                Alert("新增成功！");
            }
            else
            {
                Alert("新增失敗！");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductService productContext = new ProductService(new BizCustomerOrder());
            object unitPrice = productContext.GetProductPriceByProductID(int.Parse(ddlProducts.SelectedValue));
            if(unitPrice!=null)
            {
                labUnitPrice.Text = unitPrice.ToString();
            }
        }
    }
}