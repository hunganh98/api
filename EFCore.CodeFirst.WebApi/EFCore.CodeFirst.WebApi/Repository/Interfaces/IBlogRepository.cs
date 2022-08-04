using EFCore.CodeFirst.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Repository.Interfaces
{
    public interface IBlogRepository: IRepositoryBase<Blog>
    {
        List<Blog> GetAll();
        List<Blog> GetAllBlogs();

    }
}
