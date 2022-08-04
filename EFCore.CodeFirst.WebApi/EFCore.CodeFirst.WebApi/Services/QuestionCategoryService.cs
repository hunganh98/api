using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Services
{
    public interface IQuestionCategoryService
    {
        int GetCount();
    }
    public class QuestionCategoryService : IQuestionCategoryService
    {
        private static int count = 0;
        public QuestionCategoryService()
        {
        }
        public int GetCount()
        {
            return count;
        }
    }
}
