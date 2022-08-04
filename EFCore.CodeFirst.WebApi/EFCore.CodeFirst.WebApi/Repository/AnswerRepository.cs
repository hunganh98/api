using EFCore.CodeFirst.WebApi.Contexts;
using EFCore.CodeFirst.WebApi.Models;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Repository
{
    public class AnswerRepository : RepositoryBase<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
