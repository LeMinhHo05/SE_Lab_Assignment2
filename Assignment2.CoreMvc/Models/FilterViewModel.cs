using Microsoft.AspNetCore.Mvc.Rendering; // For SelectList
using System.ComponentModel.DataAnnotations;

namespace Assignment2.CoreMvc.Models // Or Assignment2.Core.Models if shared
{
    public class FilterViewModel
    {
        [Required(ErrorMessage = "Please select a filter type.")]
        [Display(Name = "Filter Type")]
        public string FilterType { get; set; } = "BestSellers"; // Default value

        [Display(Name = "Select Agent")]
        public int? SelectedAgentId { get; set; } // Nullable for when not needed

        public SelectList? AvailableAgents { get; set; } // To populate dropdown
    }
}