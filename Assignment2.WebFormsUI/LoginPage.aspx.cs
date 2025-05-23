using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assignment2.BLL.Services;
using Assignment2.DAL;

namespace Assignment2.WebFormsUI
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: Clear session on initial load if needed
            // if (!IsPostBack) { Session.Clear(); }
            lblErrorMessage.Visible = false; // Hide error message initially
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Please enter both username and password.");
                return;
            }

            // Use 'using' for the service
            using (UserService userService = new UserService())
            {
                User validatedUser = userService.ValidateLogin(username, password);

                if (validatedUser != null)
                {
                    // Login Successful!
                    // Store user info in Session for tracking login status
                    Session["LoggedInUser"] = validatedUser; // Store the whole object or just ID/Name
                    Session["Username"] = validatedUser.UserName;
                    FormsAuthentication.SetAuthCookie(validatedUser.UserName, false);
                    // Redirect to the main page (e.g., Default.aspx or a Dashboard.aspx)
                    Response.Redirect("Default.aspx"); // Or wherever your main content is
                }
                else
                {
                    // Login Failed
                    ShowError("Invalid username or password, or account is locked.");
                }
            }
        }

        // Helper method to show error messages
        private void ShowError(string message)
        {
            lblErrorMessage.Text = message;
            lblErrorMessage.Visible = true;
        }
    }
}