using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Models
{
    public class Question : AuditBase
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }
        public string Content { get; set; }
        public string Title { get; set; }
        public int QuestionTypeId { get; set; }
        // public int? CategoryId { get; set; }
        public int BlogId { get; set; }
        // public Category Category { get; set; }
        public QuestionType QuestionType { get; set; }
        public Blog Blog{ get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
