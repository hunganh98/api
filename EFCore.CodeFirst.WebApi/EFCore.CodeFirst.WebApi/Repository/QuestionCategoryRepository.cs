using EFCore.CodeFirst.WebApi.Models;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Repository
{
    public class QuestionCategoryRepository : IQuestionCategoryRepository
    {
        public Category Create(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> FindByCondition(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
