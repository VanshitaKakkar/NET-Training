using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Entity_Framework_Using_DataBase_approach.Models.ViewModels
{
    public class ProductDTO
    {
        public int ProductId { get; set; }  // Needed for Edit
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public string CategoryName { get; set; }
    }
}
