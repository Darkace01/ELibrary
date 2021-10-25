using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ELibrary.Core
{
    public class Category : Entity
    {
        /// <summary>
        /// Name of category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specifies if the category is a default category
        /// </summary>
        public bool DefaultCategory { get; set; }

        /// <summary>
        /// Specifies if the category is featured
        /// </summary>
        public bool IsFeatured { get; set; } = false;

        /// <summary>
        /// Associated books with the category
        /// </summary>
        public IEnumerable<Book> Books { get; set; }
    }
}
