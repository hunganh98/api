using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Models
{
    public class Blog
    {
        public Blog()
        {
            Questions = new HashSet<Question>();
        }
        public int Id { get; set; }
        public string RefId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public int LastModifiedBy { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
