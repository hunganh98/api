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
    public interface IAnswerService
    {
        AnswerViewModel CreateAnswer(AnswerViewModel newAnswer);
        AnswerViewModel UpdateAnswer(AnswerViewModel updateAnswer);
        void DeleteAnswer(int Id);

    }
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public AnswerViewModel CreateAnswer(AnswerViewModel newAnswer)
        {
            try
            {
                var answer = _mapper.Map<Answer>(newAnswer);
                if (answer.IsRightAnswer == null)
                    answer.IsRightAnswer = false;
                answer.CreatedBy = 1;
                answer.CreatedTime = DateTime.UtcNow;
                answer.LastModifiedBy = 1;
                answer.LastModifiedTime = DateTime.UtcNow;
                var addedItem = _answerRepository.Create(answer);
                return _mapper.Map<AnswerViewModel>(addedItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AnswerViewModel UpdateAnswer(AnswerViewModel answerModel)
        {
            try
            {
                var answer = _answerRepository.FindByCondition(x => x.Id == answerModel.Id).FirstOrDefault();
                if (answer != null)
                {
                    var updateAnswer = _mapper.Map<Answer>(answerModel);
                    if (updateAnswer.IsRightAnswer == null)
                        updateAnswer.IsRightAnswer = false;
                    updateAnswer.LastModifiedBy = 1;
                    updateAnswer.LastModifiedTime = DateTime.UtcNow;
                    var updatedItem = _answerRepository.Update(updateAnswer);
                    return _mapper.Map<AnswerViewModel>(updatedItem);
                }
                else
                {
                    throw new Exception("The answer does not exists");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAnswer(int Id)
        {
            try
            {
                var answer = _answerRepository.FindByCondition(x => x.Id == Id).FirstOrDefault();
                if (answer != null)
                    _answerRepository.Delete(answer);
                else
                    throw new Exception("The answer does not exists");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
