using System.ComponentModel.DataAnnotations;
namespace Assignment2.CoreMvc.Models // Or Assignment2.Core.Models
{
    public class LoginViewModels
    {
        [Required] public string UserName { get; set; }
        [Required][DataType(DataType.Password)] public string Password { get; set; }
        [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
    }
}