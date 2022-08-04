using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.DTO
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int QuestionTypeId { get; set; }
        public int CategoryId { get; set; }
        public int BlogId { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}
