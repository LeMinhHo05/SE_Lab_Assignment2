using Microsoft.AspNetCore.Http; // Required for Session
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Assignment2.CoreRazor.Pages.Account
{
    // Allow anonymous access to logout POST action if needed, or rely on AntiForgeryToken
    [IgnoreAntiforgeryToken] // Can simplify if not dealing with sensitive state change on logout itself
    public class LogoutModel : PageModel
    {
        // OnGet: Don't usually need GET for logout, redirect if accessed directly
        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }

        // OnPostAsync: Handle the logout request
        public IActionResult OnPost(string returnUrl = null)
        {
            HttpContext.Session.Clear(); // Clear session variables

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // Redirect to the home page after logout
                return RedirectToPage("/Index");
            }
        }
    }
}
