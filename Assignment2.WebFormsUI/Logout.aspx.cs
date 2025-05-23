using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment2.WebFormsUI
{
	public partial class Logout : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut(); // Sign out from Forms Auth
            Response.Redirect("LoginPage.aspx"); // Redirect to login
        }

    }
}