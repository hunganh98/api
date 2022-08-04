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
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        public BlogRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public List<Blog> GetAll()
        {
            var allQuestionsWithAnswers = this.applicationDbContext.Set<Blog>().Include(x => x.Questions).ThenInclude(y => y.Answers).ToList();
            return allQuestionsWithAnswers;
        }
        public List<Blog> GetAllBlogs()
        {
            var allBlogs = this.applicationDbContext.Set<Blog>().OrderByDescending(x => x.CreatedTime).ToList();
            return allBlogs;
        }
    }

}
