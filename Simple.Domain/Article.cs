using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Domain
{
    public class BasePoco
    {
        public int Id { get; set; }
    }
    public class Article : BasePoco
    {
        public string Heading{ get; set; }
        public string Text { get; set; }
        public int CreatedById { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}