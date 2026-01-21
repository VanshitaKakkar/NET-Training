using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Using_DataBase_approach.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }
    }
}
