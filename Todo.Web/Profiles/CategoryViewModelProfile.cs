using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Models;
using Todo.Web.ViewModels;

namespace Todo.Web.Profiles
{
    public class CategoryViewModelProfile : Profile
    {
        public CategoryViewModelProfile()
        {
            CreateMap<CategoryDao, CategoryViewModel>()
                .ReverseMap();
        }
    }
}
