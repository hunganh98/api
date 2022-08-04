using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Models
{
    public class Answer : AuditBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? IsRightAnswer { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
