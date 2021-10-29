using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.ViewModel
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFeatured { get; set; }
    }
    
    public class AddTagViewModel
    {
        public string Name { get; set; }
        public bool ValidTagName { get; set; }
    }
}
