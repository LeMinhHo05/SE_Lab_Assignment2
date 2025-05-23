using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment2.Core.Models.Data; // DbContext namespace
using Assignment2.Core.Models;

namespace Assignment2.CoreRazor.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly Assignment2.Core.Models.Data.ApplicationDbContext _context;

        public CreateModel(Assignment2.Core.Models.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AgentID"] = new SelectList(_context.Agents, "AgentID", "AgentName");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
