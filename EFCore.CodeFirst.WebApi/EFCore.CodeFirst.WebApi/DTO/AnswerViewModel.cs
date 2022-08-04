using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.DTO
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? IsRightAnswer { get; set; }
        public int QuestionId { get; set; }
    }
}
