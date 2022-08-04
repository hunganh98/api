using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Helpers;
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
    public class AnswersController : ControllerBase
    {
        private IAnswerService _answerService;
        private IUserService _userService;
        public AnswersController(IUserService userService, IAnswerService answerService)
        {
            _userService = userService;
            _answerService = answerService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddBlog(AnswerViewModel answerModel)
        {
            try
            {
                return Ok(_answerService.CreateAnswer(answerModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("update-answer")]
        public IActionResult UpdateBlog(AnswerViewModel answerModel)
        {
            try
            {
                return Ok(_answerService.UpdateAnswer(answerModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("delete-answer")]
        public IActionResult DeleteBlog([FromBody]int Id)
        {
            try
            {
                _answerService.DeleteAnswer(Id);
                return Ok(new { Status = "Item is deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
