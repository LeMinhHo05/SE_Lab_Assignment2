namespace Assignment2.CoreMvc.Models // Or Assignment2.Core.Models if shared
{
    public class BestSellerViewModel
    {
        public int ItemID { get; set; }
        public string? ItemName { get; set; } // Nullable string
        public int TotalQuantity { get; set; }
    }
}