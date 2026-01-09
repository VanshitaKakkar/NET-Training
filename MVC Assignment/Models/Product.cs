namespace MVC_Assignment.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3–50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000")]
        public decimal Price { get; set; }

        [StringLength(100, ErrorMessage = "Description max 100 characters")]
        public string Description { get; set; }

     
        public Product()
        {
        }

      
        public Product(int productId, string name, decimal price, string description = "")
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Description = description;
        }
    }
}
