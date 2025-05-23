using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Core.Models
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; } // Simple PK for the detail line

        // Foreign Key property for Order
        [Required]
        public int OrderID { get; set; }

        // Foreign Key property for Item
        [Required]
        public int ItemID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be positive.")]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] // Explicitly map to SQL decimal
        [Range(0, double.MaxValue, ErrorMessage = "Unit Amount cannot be negative.")]
        public decimal UnitAmount { get; set; }

        // Navigation properties back to Order and Item
        [ForeignKey("OrderID")]
        public Order? Order { get; set; }

        [ForeignKey("ItemID")]
        public Item? Item { get; set; }
    }
}
