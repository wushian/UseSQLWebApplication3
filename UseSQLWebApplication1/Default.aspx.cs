using Cus.Business.Order;
using Cus.DataAccess.Order;
using Cus.Services.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UseSQLWebApplication1
{
    public partial class Default : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                CreateList();
            }
        }

        private void CreateList()
        {
            CustomerService cusContext = new CustomerService(new BizCustomerOrder());
            ddlCusContacts.DataSource = cusContext.GetCustomerList();
            ddlCusContacts.DataValueField = "CustomerID";
            ddlCusContacts.DataTextField = "ContactName";
            ddlCusContacts.DataBind();
        }

        protected void btnFindOrderByCus_Click(object sender, EventArgs e)
        {
            GetOrderDataByCus(ddlCusContacts.SelectedValue);
        }

        private void GetOrderDataByCus(string CusID)
        {
            CustomerService cusContext = new CustomerService(new BizCustomerOrder());
            gvOrderDetails.DataSource = cusContext.GetByCusID(CusID);
            gvOrderDetails.DataBind();
        }

        protected void btnAddOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("AddOrder.aspx?customerid={0}", ddlCusContacts.SelectedValue));
        }
    }
}