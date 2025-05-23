using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment2.Core.Models.Data; // DbContext namespace
using Assignment2.Core.Models;

namespace Assignment2.CoreRazor.Pages.Agents
{
    public class DetailsModel : PageModel
    {
        private readonly Assignment2.Core.Models.Data.ApplicationDbContext _context;

        public DetailsModel(Assignment2.Core.Models.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Agent Agent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.FirstOrDefaultAsync(m => m.AgentID == id);
            if (agent == null)
            {
                return NotFound();
            }
            else
            {
                Agent = agent;
            }
            return Page();
        }
    }
}
