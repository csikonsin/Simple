using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Domain
{
    public class Menu : BasePoco
    {
        public string PublicUrl { get; set; }

        public int? RedirectMenuId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedById { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedById { get; set; }
    }
}
