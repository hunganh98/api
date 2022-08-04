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
    public class QuizesController : ControllerBase
    {
        private IQuizService _quizService;
        private IUserService _userService;
        private IBlogService _blogService;
        private IQuestionTypeService _questionTypeService;

        public QuizesController(IQuizService quizService, IUserService userService, IBlogService blogService, IQuestionTypeService questionTypeService)
        {
            _quizService = quizService;
            _userService = userService;
            _blogService = blogService;
            _questionTypeService = questionTypeService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = new List<BlogAPIViewModel>();
                var blogs = _blogService.GetAll().ToList();
                if (blogs.Count == 0) return Ok(result);
                foreach (var blog in blogs)
                {
                    var item = new BlogAPIViewModel();
                    item.RefId = blog.RefId;

                    var questions = blog.Questions.ToList();
                    foreach (var ques in questions)
                    {
                        var sortedAnswers = ques.Answers.OrderBy(z => z.Title).ToList();
                        ques.Answers = sortedAnswers;
                    }
                    string quizBlock = @"";
                    item.RightAnswers = questions?.SelectMany(x => x.Answers)?.Where(a => a.IsRightAnswer == true).Select(y => y.Id)?.ToList();
                    item.WrongAnswers = questions?.SelectMany(x => x.Answers)?.Where(a => a.IsRightAnswer == false).Select(y => y.Id)?.ToList();
                    item.NoQuestion = questions.Count();

                    string submitButton = 
                        "<div style = 'display: flex'>" +
                            $@"<button type='button'" +
                            " onclick=\"displayAnswer(" + $"\'{blog.RefId }\'" + 
                            ")\" style='width: 100px; height: 40px; border-radius: 3px; background-color: #F69E31; font-weight: 700; margin-right: 20px'>Submit</button>" +
                            $@"<div id='score-wrapper-{blog.RefId}' style = 'line-height: 40px; display: none'>" +
                                "<span style = 'margin-right: 10px; font-weight: 600' > Score </span>" +
                                $@"<input style='width: 100px; height: 30px; text-align: center' type='text' value='' id='score-{blog.RefId}'/>
                            </div>"+
                         "</div>";
                    if (questions.Count > 0)
                    {
                        for (int i = 1; i <= questions.Count; i++)
                        {
                            var quesElement = $@"<pre style='font-size: 1rem;font-family: Asap ;'><b>Question {i}</b>.    {questions[i - 1].Content}</pre>";
                            var answerElement = @"";
                            foreach (var asw in questions[i - 1].Answers)
                            {
                                if (asw.IsRightAnswer == true)
                                {
                                    answerElement += $@"
                            <div id = 'blk-cte-01-aws-{asw.Id}' style='padding: 10px; width: max-content;'>
                                <label for='cte-1ri1-{asw.Id}' style=' padding: 5px; font-size: 1rem;'>
                                    <input type='radio' name='option-qs-{questions[i - 1].Id}' value='{asw.Content}' id='cte-1ri1-{asw.Id}' 
                                        style='transform: scale(1.6); margin-right: 10px; vertical-align: middle; margin-top: -2px;' /> 
                                    {asw.Title}.    {asw.Content}
                                </label>
                                <span id='result-{asw.Id}'></span>
                             </div>";
                                }
                                else
                                {
                                    answerElement += $@"
                            <div id = 'blk-cte-00-aws-{asw.Id}' style='padding: 10px; width: max-content;'>
                                <label for='cte-0wr0-{asw.Id}' style=' padding: 5px; font-size: 1rem;'>
                                    <input type='radio' name='option-qs-{questions[i - 1].Id}' value='{asw.Content}' id='cte-0wr0-{asw.Id}' 
                                        style='transform: scale(1.6); margin-right: 10px; vertical-align: middle; margin-top: -2px;' /> 
                                    {asw.Title}.    {asw.Content}
                                </label>
                                <span id='result-{asw.Id}'></span>
                             </div>";
                                }
                            }
                            quizBlock += quesElement + $@"<div>{answerElement}</div>";
                        }
                        quizBlock += @"<br/>" + submitButton;
                    }
                    item.Template = $@"<div>{quizBlock}</div>";
                    result.Add(item);
                }

                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("{blogId}")]
        public IActionResult GetQuizzesByBlog(int blogId)
        {
            var quizzes =  _quizService.GetQuestionsByBlog(blogId);
            return Ok(quizzes);
        }

        
        [Authorize]
        [HttpPost]
        public IActionResult CreateQuiz(QuestionViewModel newQuiz)
        {
            var quizzes = _quizService.CreateNewQuiz(newQuiz);
            return Ok(quizzes);
        }
      
        [Authorize]
        [HttpPost]
        [Route("update-quiz")]
        public IActionResult UpdateQuiz(QuestionViewModel quizModel)
        {
            var quizzes = _quizService.UpdateNewQuiz(quizModel);
            return Ok(quizzes);
        }

        [Authorize]
        [HttpPost]
        [Route("delete-quiz")]
        public IActionResult DeleteQuestion([FromBody]int Id)
        {
            try
            {
                _quizService.DeleteQuestion(Id);
                return Ok(new { Status = "Item is deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("QuestionTypes")]
        public IActionResult QuestionTypes()
        {
            var types = _questionTypeService.GetBlogLookupData();
            return Ok(types);
        }
    }
}
