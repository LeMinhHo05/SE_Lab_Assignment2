using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment2.Core.Models.Data; // DbContext namespace
using Assignment2.Core.Models; 

namespace Assignment2.CoreRazor.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Assignment2.Core.Models.Data.ApplicationDbContext _context;

        public IndexModel(Assignment2.Core.Models.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Order = await _context.Orders
                  .Include(o => o.Agent) // Eager load Agent
                  .OrderByDescending(o => o.OrderDate) // Optional: Order by date
                  .ToListAsync();
        }
    }
}
