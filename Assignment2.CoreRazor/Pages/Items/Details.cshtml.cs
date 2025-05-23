using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment2.Core.Models.Data; // DbContext namespace
using Assignment2.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment2.CoreRazor.Pages.Items
{
    
    public class DetailsModel : PageModel
    {
        private readonly Assignment2.Core.Models.Data.ApplicationDbContext _context;

        public DetailsModel(Assignment2.Core.Models.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Item Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FirstOrDefaultAsync(m => m.ItemID == id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                Item = item;
            }
            return Page();
        }
    }
}
