using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.CoreRazor.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        [StringLength(200)]
        public string ItemName { get; set; }

        [StringLength(50)]
        public string? Size { get; set; } // Nullable string for optional size

        // Navigation property for related OrderDetails
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
