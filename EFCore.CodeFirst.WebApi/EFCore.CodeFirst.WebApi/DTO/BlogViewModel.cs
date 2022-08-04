using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.DTO
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string RefId { get; set; }
        public string Name { get; set; }
    }
}
