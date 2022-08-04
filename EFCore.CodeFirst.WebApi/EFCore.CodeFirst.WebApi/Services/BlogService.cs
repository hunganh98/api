using AutoMapper;
using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Models;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Services
{

    public interface IBlogService
    {
        List<Blog> GetAll();
        List<BlogViewModel> GetAllBlogs();
        List<BlogLookupViewModel> GetBlogLookupData();
        BlogViewModel CreateBlog(BlogViewModel newBlog, int userId);
        BlogViewModel UpdateBlog(BlogViewModel updateBlog, int userId);
        void DeleteBlog(int Id);

    }
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public List<Blog> GetAll()
        {
            return _blogRepository.GetAll().ToList();
        }
        public List<BlogViewModel> GetAllBlogs()
        {
            return _mapper.Map<List<BlogViewModel>>(_blogRepository.GetAllBlogs());
        }

        public BlogViewModel CreateBlog(BlogViewModel newBlog, int userId)
        {
            var blog = _mapper.Map<Blog>(newBlog);
            blog.CreatedBy = userId;
            blog.CreatedTime = DateTime.UtcNow;
            blog.LastModifiedBy = userId;
            blog.LastModifiedTime = DateTime.UtcNow;
            var addedItem = _blogRepository.Create(blog);
            return _mapper.Map<BlogViewModel>(addedItem);
        }

        public BlogViewModel UpdateBlog(BlogViewModel updateBlog, int userId )
        {
            var blog = _mapper.Map<Blog>(updateBlog);
            blog.LastModifiedBy = userId;
            blog.LastModifiedTime = DateTime.UtcNow;
            var updatedItem = userId;
            return _mapper.Map<BlogViewModel>(updatedItem);
        }
        public void DeleteBlog(int Id)
        {
            try
            {
                var blog = _blogRepository.FindByCondition(x => x.Id == Id).FirstOrDefault();
                if (blog != null)
                    _blogRepository.Delete(blog);
                else
                    throw new Exception("The blog does not exists");
            } 
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public List<BlogLookupViewModel> GetBlogLookupData()
        {
            return _mapper.Map<List<BlogLookupViewModel>>(_blogRepository.GetAllBlogs());
        }
    }

}
