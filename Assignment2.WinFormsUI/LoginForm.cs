using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assignment2.BLL.Services; 
using Assignment2.DAL;

namespace Assignment2.WinFormsUI
{
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Get username, remove whitespace
            string password = txtPassword.Text;      // Get password

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop processing if fields are empty
            }

            // Use 'using' statement for UserService to ensure it's disposed properly
            using (UserService userService = new UserService())
            {
                User validatedUser = userService.ValidateLogin(username, password);

                if (validatedUser != null)
                {
                    // Login Successful!
                    MessageBox.Show($"Welcome, {validatedUser.UserName}!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // TODO: Open the Main Form and hide this Login Form
                    // We will create MainForm next
                    MainForm mainForm = new MainForm(); // Create instance of MainForm
                    mainForm.Show();                     // Show the MainForm
                    this.Hide();                         // Hide the LoginForm
                }
                else
                {
                    // Login Failed
                    MessageBox.Show("Invalid username or password, or account is locked.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear(); // Clear password field after failed attempt
                    txtUsername.Focus(); // Set focus back to username
                }
            } // UserService is disposed here
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Optional: Handle form closing to ensure application exits if login isn't successful
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the user closes the login form directly (not via successful login or Cancel), exit the app
            // Check if the reason for closing is user action and not code initiated (like this.Hide())
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Check if any other forms are open (like MainForm) before exiting
                // This check prevents exiting if login was successful and MainForm is shown
                if (Application.OpenForms.Count <= 1) // Only this form is open
                {
                    Application.Exit();
                }
                // If MainForm is open, closing Login form shouldn't exit the app
            }
        }
    }
}
