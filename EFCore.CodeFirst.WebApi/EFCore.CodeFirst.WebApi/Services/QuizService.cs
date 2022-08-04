using AutoMapper;
using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Models;
using EFCore.CodeFirst.WebApi.Repository;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Services
{
    public interface IQuizService
    {
        List<Blog> GetAll();
        List<QuestionViewModel> GetQuestionsByBlog(int blogId);
        QuestionViewModel CreateNewQuiz(QuestionViewModel newQuiz);
        QuestionViewModel UpdateNewQuiz(QuestionViewModel updateQuiz);
        void DeleteQuestion(int Id);


    }
    public class QuizService : IQuizService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public QuizService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public List<Blog> GetAll()
        {
            return _questionRepository.GetAll().ToList();
        } 

        public List<QuestionViewModel> GetQuestionsByBlog(int blogId)
        {
            var questions = _questionRepository.GetQuestionsByBlogId(blogId);
            List<QuestionViewModel> result = _mapper.Map<List<QuestionViewModel>>(questions);
            return result;
        }

        public QuestionViewModel UpdateNewQuiz(QuestionViewModel updateQuiz)
        {
            try
            {
                var blog = _questionRepository.FindByCondition(x => x.Id == updateQuiz.Id).FirstOrDefault();
                if (blog != null)
                {
                    var quiz = _mapper.Map<Question>(updateQuiz);
                    quiz.LastModifiedBy = 1;
                    quiz.LastModifiedTime = DateTime.UtcNow;
                    var updatedItem = _questionRepository.Update(quiz);
                    return _mapper.Map<QuestionViewModel>(updatedItem);
                } else
                {
                    throw new Exception("The quiz does not exists");
                }
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public QuestionViewModel CreateNewQuiz(QuestionViewModel newQuiz)
        {
            try
            {
                var quiz = _mapper.Map<Question>(newQuiz);
                quiz.CreatedBy = 1;
                quiz.CreatedTime = DateTime.UtcNow;
                quiz.LastModifiedBy = 1;
                quiz.LastModifiedTime = DateTime.UtcNow;
                var addedItem = _questionRepository.Create(quiz);
                return _mapper.Map<QuestionViewModel>(addedItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteQuestion(int Id)
        {
            try
            {
                var blog = _questionRepository.FindByCondition(x => x.Id == Id).FirstOrDefault();
                if (blog != null)
                    _questionRepository.Delete(blog);
                else
                    throw new Exception("The quiz does not exists");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
