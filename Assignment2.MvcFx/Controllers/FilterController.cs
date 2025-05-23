using Assignment2.MvcFx.Models; // Access DbContext and entities
using System;
using System.Collections.Generic;
using System.Data.Entity; // For Include
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations; // Add this for the [Key] attribute


namespace Assignment2.MvcFx.Controllers
{
    [Authorize] // Only logged-in users can access reports
    public class FilterController : Controller
    {
        private SE_Assignment2_DBEntities db = new SE_Assignment2_DBEntities(); // Instantiate DbContext

        // GET: Filter/Index - Display the filter form
        public ActionResult Index()
        {
            // Prepare data needed for the form (e.g., Agent list for dropdown)
            var viewModel = new FilterViewModel
            {
                // Populate agents for the dropdown, add a default option
                AvailableAgents = new SelectList(db.Agents.OrderBy(a => a.AgentName).ToList(), "AgentID", "AgentName")
            };
            return View(viewModel);
        }

        // POST: Filter/GenerateReport - Process form submission and display results
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateReport(FilterViewModel model)
        {
            // Repopulate dropdowns in case we need to return the view on error
            model.AvailableAgents = new SelectList(db.Agents.OrderBy(a => a.AgentName).ToList(), "AgentID", "AgentName", model.SelectedAgentId);

            if (!ModelState.IsValid)
            {
                // If basic model validation fails (e.g., required fields missing if added), return form
                return View("Index", model);
            }

            // --- Execute Filter Logic ---
            object results = null; // Use object to hold results of different types
            string reportTitle = "Report Results";

            try
            {
                if (model.FilterType == "BestSellers")
                {
                    reportTitle = "Best Selling Items (Top 10)";
                    results = db.OrderDetails
                                    .GroupBy(od => od.Item)
                                    .Select(g => new BestSellerViewModel // Use a ViewModel
                                    {
                                        ItemID = g.Key.ItemID,
                                        ItemName = g.Key.ItemName,
                                        TotalQuantity = g.Sum(od => od.Quantity)
                                    })
                                    .OrderByDescending(r => r.TotalQuantity)
                                    .Take(10)
                                    .ToList();
                }
                else if (model.FilterType == "AgentHistory")
                {
                    reportTitle = "Agent Order History";
                    if (!model.SelectedAgentId.HasValue)
                    {
                        ModelState.AddModelError("SelectedAgentId", "Please select an agent for this report.");
                        return View("Index", model);
                    }
                    results = db.OrderDetails
                                    .Include(od => od.Order) // Include Order for AgentID and Date
                                    .Include(od => od.Item)  // Include Item for Name
                                    .Where(od => od.Order.AgentID == model.SelectedAgentId.Value)
                                    .Select(od => new AgentHistoryViewModel // Use a ViewModel
                                    {
                                        OrderDate = od.Order.OrderDate,
                                        ItemName = od.Item.ItemName,
                                        Quantity = od.Quantity,
                                        UnitAmount = od.UnitAmount,
                                        LineTotal = od.Quantity * od.UnitAmount
                                    })
                                    .OrderByDescending(r => r.OrderDate)
                                    .ToList();
                }
                else
                {
                    ModelState.AddModelError("FilterType", "Invalid filter type selected.");
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                // Log the exception ex
                ModelState.AddModelError("", "An error occurred while generating the report.");
                return View("Index", model);
            }

            // Pass results and type to a generic Report Results view
            ViewBag.ReportTitle = reportTitle;
            ViewBag.FilterType = model.FilterType; // Pass filter type to view for conditional display
            return View("ReportResults", results); // Pass the results (List<ViewModel>) to the view
        }

        // Dispose DbContext when controller is disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    // --- ViewModels for Filter Form and Results ---

    public class FilterViewModel
    {
        [Key] // Add this attribute to define a key
        public string FilterType { get; set; } // e.g., "BestSellers", "AgentHistory"

        [Display(Name = "Select Agent")]
        public int? SelectedAgentId { get; set; } // Nullable int for when agent isn't needed

        // For populating the Agent dropdown
        public SelectList AvailableAgents { get; set; }
    }

    // ViewModel for Best Seller Results
    public class BestSellerViewModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int TotalQuantity { get; set; }
    }

    // ViewModel for Agent History Results
    public class AgentHistoryViewModel
    {
        public DateTime OrderDate { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal LineTotal { get; set; }
    }
}