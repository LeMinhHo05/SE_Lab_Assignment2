using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Core.Models
{
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }

        [Required]
        [StringLength(150)]
        public string AgentName { get; set; }

        [StringLength(255)]
        public string? Address { get; set; } // Nullable

        // Navigation property for related Orders
        public ICollection<Order>? Orders { get; set; }
    }
}