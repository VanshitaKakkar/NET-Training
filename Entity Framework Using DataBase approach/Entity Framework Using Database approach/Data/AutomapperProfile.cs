namespace Entity_Framework_Using_DataBase_approach.Data
{
    using AutoMapper;
    using global::Entity_Framework_Using_DataBase_approach.Models;
    using global::Entity_Framework_Using_DataBase_approach.Models.ViewModels;

    namespace Entity_Framework_Using_DataBase_approach.Mapping
    {
        public class AutomapperProfile : Profile
        {
            public AutomapperProfile()
            {
                // Entity -> ViewModel
                CreateMap<Product, ProductDTO>()
                    .ForMember(dest => dest.CategoryName,
                               opt => opt.MapFrom(src => src.Category.CategoryName));
                 CreateMap<ProductDTO, Product>();
                CreateMap<Category, CategoryDTO>().ReverseMap();
            }
        }
    }

}
