using Assignment2.Core.Models.Data; // DbContext namespace
using Assignment2.Core.Models; // ViewModels and Entities namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // For FirstOrDefaultAsync
using System.Threading.Tasks; // For Async methods
// Required for Session
using Microsoft.AspNetCore.Http;


namespace Assignment2.CoreRazor.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // BindProperty supports POST requests by default
        [BindProperty]
        public LoginViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData] // TempData persists for one redirect
        public string ErrorMessage { get; set; }

        // OnGet: Prepare the page (e.g., capture return URL)
        public void OnGet(string returnUrl = null)
        {
            // Clear existing session/cookie if needed (more robust in real Identity)
            // HttpContext.Session.Clear(); // Requires Session configured

            ReturnUrl = returnUrl ?? Url.Content("~/"); // Default redirect to home page
        }

        // OnPostAsync: Handle the form submission
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid) // Check if model binding and validation annotations passed
            {
                // IMPORTANT: HASH the input password before comparing in a real app!
                // var hashedPassword = HashPassword(Input.Password); // Implement hashing
                var user = await _context.Users
                                   .FirstOrDefaultAsync(u => u.UserName == Input.UserName && u.Password == Input.Password); // Compare plain text for now

                if (user != null)
                {
                    if (user.Lock)
                    {
                        ModelState.AddModelError(string.Empty, "Account locked.");
                        // Or use TempData for error message on redirect
                        // ErrorMessage = "Account locked.";
                        return Page(); // Return to login page with error
                    }

                    // --- Login Successful ---
                    // Store user info in Session. Requires Session configured in Program.cs
                    HttpContext.Session.SetInt32("UserID", user.UserID);
                    HttpContext.Session.SetString("Username", user.UserName);

                    // Redirect to the original requested URL or the home page
                    return LocalRedirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    // Or use TempData
                    // ErrorMessage = "Invalid login attempt.";
                    return Page(); // Return to login page with error
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}