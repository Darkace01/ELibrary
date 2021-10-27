using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
