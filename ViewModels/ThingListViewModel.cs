using shhop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shhop.ViewModels
{
    public class ThingListViewModel
    {
        public IEnumerable<Thing> allThings { get; set; }

        public string currCategory { get; set; }

        public string currCategoryItem { get; set; }
    }
}
