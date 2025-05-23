using System.ComponentModel.DataAnnotations;

namespace Assignment2.Core.Models
{
    public class User
    {
        [Key] // Primary Key
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        // Consider adding Index for uniqueness if needed, requires EF Core config
        public string Email { get; set; }

        [Required]
        [StringLength(255)] // Allow ample space for hashed passwords
        public string Password { get; set; } // Store HASHED passwords

        [Required]
        public bool Lock { get; set; } = false; // Default value
    }
}