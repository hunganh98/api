using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Models
{
    public class AuditBase : Entity
    {
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedTime{ get; set; }
        public int LastModifiedBy { get; set; }

    }
}
