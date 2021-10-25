using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Core
{
    public class Book : Entity
    {
        /// <summary>
        /// Name of the book
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Library Code of book
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Id of the cateogry
        /// </summary>
        public int? CategoryId { get; set; }
        /// <summary>
        /// Cateogry of book
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// List of related tags seperated by comma
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// Image Url
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Book Author
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Book description
        /// </summary>
        public string Description { get; set; }
    }
}
