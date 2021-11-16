using AutoMapper;
using ELibrary.Core;
using ELibrary.ViewModel;

namespace ELibrary.Utility;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // From Model => To View Model
        // Book Map
        CreateMap<Book, BookViewModel>().ReverseMap();
        CreateMap<Book, EditBookViewModel>().ReverseMap();
        CreateMap<AddBookViewModel, Book>().ReverseMap();

        // Category Map
        CreateMap<Category, CategoryViewModel>()
            .ForMember(c => c.NoOfBooks, opt => opt.MapFrom(src => src.Books.Count()));
        CreateMap<AddCategoryViewModel, Category>();
        CreateMap<Category, EditCategoryViewModel>().ReverseMap();

        //Application User
        CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
    }
}
