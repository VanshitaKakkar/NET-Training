using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Using_DataBase_approach.Models.ViewModels
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [ValidateNever]
        public ICollection<ProductDTO> Products { get; set; }
    }
}
