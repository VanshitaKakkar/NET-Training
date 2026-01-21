using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Using_DataBase_approach.Models.ViewModels
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
