using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shhop.Models
{
    public class Thing
    {
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string img { get; set; }

        public string author { get; set; }

        public ushort price { get; set; }
        public int categoryId { get; set; }
        public virtual Category category { get; set; }

    }
}
