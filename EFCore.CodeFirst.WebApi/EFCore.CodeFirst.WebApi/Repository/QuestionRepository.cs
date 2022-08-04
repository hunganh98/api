using EFCore.CodeFirst.WebApi.Contexts;
using EFCore.CodeFirst.WebApi.Models;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Repository
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext){
        }

        public List<Blog> GetAll()
        {
            var allQuestionsWithAnswers = this.applicationDbContext.Set<Blog>().Include(x => x.Questions).ThenInclude(y => y.Answers).ToList();
            return allQuestionsWithAnswers;
        }

        public List<Question> GetQuestionsByBlogId(int blogId)
        {
            var allQuestionsWithAnswers = this.applicationDbContext.Set<Question>().Where(x => x.BlogId == blogId).Include(y => y.Answers).ToList();
            foreach (var ques in allQuestionsWithAnswers)
            {
                var sortedAnswers  = ques.Answers.OrderBy(z => z.Title).ToList();
                ques.Answers = sortedAnswers;
            }

            return allQuestionsWithAnswers;
        }
    }
}
