using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Models;

namespace Todo.Business.Profiles
{
    public class TodoItemTagVoProfile : Profile
    {
        public TodoItemTagVoProfile()
        {
            CreateMap<TodoItemTagDao, TodoItemTagVo>()
                .ReverseMap();
        }
    }
}
