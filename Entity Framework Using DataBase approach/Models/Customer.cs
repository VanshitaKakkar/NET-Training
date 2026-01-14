using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Using_DataBase_approach.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Navigation Property
        public ICollection<Order> Orders { get; set; }
    }
}
