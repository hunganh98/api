using AutoMapper;
using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Blog, BlogViewModel>().ReverseMap();
            CreateMap<Blog, BlogLookupViewModel>().ReverseMap();
            CreateMap<Question, QuestionViewModel>().ReverseMap();
            CreateMap<Answer, AnswerViewModel>().ReverseMap();
            CreateMap<QuestionType, QuestionTypeViewModel>().ReverseMap();
            
        }
    }
}
