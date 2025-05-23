using Assignment2.Core.Models;      // For entity models (User, Item, Agent, Order, OrderDetail)
using Assignment2.Core.Models.Data; // For ApplicationDbContext
using Assignment2.CoreMvc.Models;   // For FilterViewModel, BestSellerViewModel, AgentHistoryViewModel
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.CoreMvc.Controllers
{
     // Ensure only logged-in users can access
    public class FilterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilterController(ApplicationDbContext context)
        {
            _context = context; // Inject DbContext
        }

        // GET: Filter or Filter/Index
        public async Task<IActionResult> Index()
        {
            var viewModel = new FilterViewModel
            {
                // Populate agents for the dropdown
                AvailableAgents = new SelectList(await _context.Agents.OrderBy(a => a.AgentName).ToListAsync(), "AgentID", "AgentName")
            };
            return View(viewModel);
        }

        // POST: Filter/GenerateReport (or could be Filter/Index if form posts to Index)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateReport(FilterViewModel model)
        {
            // Repopulate dropdown in case we need to return the view with errors
            model.AvailableAgents = new SelectList(await _context.Agents.OrderBy(a => a.AgentName).ToListAsync(), "AgentID", "AgentName", model.SelectedAgentId);

            // Check model state for basic validation (e.g., FilterType required)
            if (!ModelState.IsValid)
            {
                return View("Index", model); // Return to the form view with validation errors
            }

            object results = null; // To hold results of different types
            string reportTitle = "Report Results";

            try
            {
                if (model.FilterType == "BestSellers")
                {
                    reportTitle = "Best Selling Items (Top 10)";
                    results = await _context.OrderDetails
                                .Include(od => od.Item) // Make sure Item is included
                                .Where(od => od.Item != null) // Safety check
                                .GroupBy(od => od.Item)
                                .Select(g => new BestSellerViewModel
                                {
                                    ItemID = g.Key.ItemID,
                                    ItemName = g.Key.ItemName,
                                    TotalQuantity = g.Sum(od => od.Quantity)
                                })
                                .OrderByDescending(r => r.TotalQuantity)
                                .Take(10)
                                .ToListAsync();
                }
                else if (model.FilterType == "AgentHistory")
                {
                    reportTitle = "Agent Order History";
                    if (!model.SelectedAgentId.HasValue || model.SelectedAgentId.Value == 0) // Also check for default unselected value
                    {
                        ModelState.AddModelError(nameof(model.SelectedAgentId), "Please select an agent for this report.");
                        return View("Index", model);
                    }
                    results = await _context.OrderDetails
                                .Include(od => od.Order)
                                .Include(od => od.Item)
                                .Where(od => od.Order != null && od.Item != null && od.Order.AgentID == model.SelectedAgentId.Value)
                                .Select(od => new AgentHistoryViewModel
                                {
                                    OrderDate = od.Order.OrderDate,
                                    ItemName = od.Item.ItemName,
                                    Quantity = od.Quantity,
                                    UnitAmount = od.UnitAmount,
                                    LineTotal = od.Quantity * od.UnitAmount
                                })
                                .OrderByDescending(r => r.OrderDate)
                                .ToListAsync();
                }
                else
                {
                    ModelState.AddModelError(nameof(model.FilterType), "Invalid filter type selected.");
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) in a real application
                ModelState.AddModelError("", $"An error occurred while generating the report: {ex.Message}");
                return View("Index", model); // Return to form view with general error
            }

            ViewBag.ReportTitle = reportTitle;
            ViewBag.FilterType = model.FilterType; // Pass filter type to view for conditional display
            return View("ReportResults", results); // Pass the results to the ReportResults view
        }
    }
}