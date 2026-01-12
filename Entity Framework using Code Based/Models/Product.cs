using System.ComponentModel.DataAnnotations;

namespace Entity_Framework.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        
       

    }
}
