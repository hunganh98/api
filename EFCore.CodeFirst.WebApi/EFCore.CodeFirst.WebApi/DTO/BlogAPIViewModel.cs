using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.DTO
{
    public class BlogAPIViewModel
    {
        public string RefId { get; set; }
        public string Template { get; set; }
        public List<int> RightAnswers { get; set; }
        public List<int> WrongAnswers { get; set; }
        public int NoQuestion { get; set; }
    }
}
