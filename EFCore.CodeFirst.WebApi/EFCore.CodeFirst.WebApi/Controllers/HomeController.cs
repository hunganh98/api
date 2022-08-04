using EFCore.CodeFirst.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Controllers
{
    [Route("home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var a = _userService.NumberOfUsers();
                return Ok("Hello world "+a);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
