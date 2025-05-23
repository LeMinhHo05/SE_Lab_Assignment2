using Assignment2.MvcFx.Models; // Access generated entities and DbContext
using System.Linq;
using System.Web.Mvc;
using System.Web.Security; // For FormsAuthentication

namespace Assignment2.MvcFx.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [AllowAnonymous] // Allow access even if not logged in
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl; // Store original requested URL if redirected
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return view with validation errors
            }

            // Use 'using' for DbContext
            using (var db = new SE_Assignment2_DBEntities()) // Use your actual DbContext name
            {
                // Find user matching credentials (case-sensitive)
                // IMPORTANT: Compare HASHED passwords in a real app!
                var user = db.Users.FirstOrDefault(u => u.UserName == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    if (user.Lock == true)
                    {
                        ModelState.AddModelError("", "This account is locked.");
                        return View(model);
                    }

                    // --- Login Successful ---
                    // Use Forms Authentication to set a cookie indicating the user is logged in
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe); // Set cookie

                    // Store minimal info in Session if needed (optional)
                    Session["UserID"] = user.UserID;
                    Session["Username"] = user.UserName;


                    // Redirect to the original requested URL or default page
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    // --- Login Failed ---
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut(); // Clear authentication cookie
            Session.Clear(); // Clear session data
            Session.Abandon();
            return RedirectToAction("Index", "Home"); // Redirect to home page
        }


        // Helper method to prevent open redirect attacks
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home"); // Redirect to default page
        }
    }

    // ViewModel for the Login page
    public class LoginViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Username")]
        public string Username { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [System.ComponentModel.DataAnnotations.Display(Name = "Password")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}