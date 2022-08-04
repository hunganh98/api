using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Models
{
    public class QuestionType : AuditBase
    {
        public string QuestionTypeName { get; set; }
        [MaxLength(10)]
        public string QuestionTypeCode { get; set; }

    }
}
