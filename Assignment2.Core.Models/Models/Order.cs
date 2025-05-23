using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // For ForeignKey attribute

namespace Assignment2.Core.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        // Foreign Key property
        [Required]
        public int AgentID { get; set; }

        // Navigation property back to the Agent
        [ForeignKey("AgentID")]
        public Agent? Agent { get; set; }

        // Navigation property for related OrderDetails
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
