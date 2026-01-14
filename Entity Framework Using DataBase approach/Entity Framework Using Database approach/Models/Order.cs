using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Using_DataBase_approach.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // Foreign Key
        public int CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        // Navigation Property
        public Customer Customer { get; set; }
    }
}
