using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Business.Models;
using Todo.Web.ViewModels;

namespace Todo.Web.Profiles
{
    public class TodoItemViewModelProfile : Profile
    {
        public TodoItemViewModelProfile()
        {
            CreateMap<TodoItemDao, TodoItemViewModel>()
                .ReverseMap();
        }
    }
}
