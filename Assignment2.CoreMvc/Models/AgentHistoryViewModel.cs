using System;
using System.ComponentModel.DataAnnotations; // For DisplayFormat

namespace Assignment2.CoreMvc.Models // Or Assignment2.Core.Models if shared
{
    public class AgentHistoryViewModel
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime OrderDate { get; set; }
        public string? ItemName { get; set; } // Nullable string
        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")] // Currency format
        public decimal UnitAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")] // Currency format
        public decimal LineTotal { get; set; }
    }
}