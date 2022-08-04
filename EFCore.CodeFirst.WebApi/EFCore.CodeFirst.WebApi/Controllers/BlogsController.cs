using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Helpers;
using EFCore.CodeFirst.WebApi.Models;
using EFCore.CodeFirst.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private IBlogService _blogService;
        private IUserService _userService;
        public BlogsController(IUserService userService, IBlogService blogService)
        {
            _userService = userService;
            _blogService = blogService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        { 
            return Ok(_blogService.GetAllBlogs());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddBlog(BlogViewModel blogViewModel)
        {
            try
            {
                var user = (User)HttpContext.Items["User"];
                if (user == null)
                {
                    return BadRequest("Invalid  User");
                }
                return Ok(_blogService.CreateBlog(blogViewModel, user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while adding a blog");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("update-blog")]
        public IActionResult UpdateBlog(BlogViewModel blogViewModel)
        {
            try
            {
                var user = (User)HttpContext.Items["User"];
                if (user == null)
                {
                    return BadRequest("Invalid  User");
                }
                return Ok(_blogService.UpdateBlog(blogViewModel, user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while updating a blog");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("delete-blog")]
        public IActionResult DeleteBlog([FromBody]int Id)
        {
            try
            {
                _blogService.DeleteBlog(Id);
                return Ok(new {Status = "Item is deleted"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Lookup")]
        public IActionResult GetLookupData()
        {
            return Ok(_blogService.GetBlogLookupData());
        }
    }
}
