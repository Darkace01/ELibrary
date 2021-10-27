using AutoMapper;
using ELibrary.Core;
using ELibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // From Model => To View Model
            // Book Map
            CreateMap<Book, BookViewModel>().ReverseMap();


            // Category Map
            CreateMap<Category, CategoryViewModel>();
            CreateMap<AddCategoryViewModel, Category>();
            CreateMap<Category, EditCategoryViewModel>().ReverseMap();
        }
    }
}
