using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Domain
{
    public class Module : BasePoco
    {
        public int MenuId { get; set; }

        public int ModuleId { get; set; }

        public string Parameter { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
