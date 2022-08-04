using AutoMapper;
using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Services
{
    public interface IQuestionTypeService
    {
        List<QuestionTypeViewModel> GetBlogLookupData();


    }
    public class QuestionTypeService : IQuestionTypeService
    {
        private readonly IQuestionTypeRepository _questionTypeRepository;
        private readonly IQuestionCategoryRepository _test;
        private readonly IMapper _mapper;
        public QuestionTypeService(IQuestionTypeRepository questionTypeRepository, IMapper mapper, IQuestionCategoryRepository test)
        {
            _questionTypeRepository = questionTypeRepository;
            _test = test;
            _mapper = mapper;
        }

        public List<QuestionTypeViewModel> GetBlogLookupData()
        {
            return _mapper.Map<List<QuestionTypeViewModel>>(_questionTypeRepository.GetAll());
        }
    }
}
