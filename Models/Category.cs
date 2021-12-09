using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shhop.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int categoryItemId { get; set; }

        public virtual CategoryItem categoryItem { get; set; }
    }
}
