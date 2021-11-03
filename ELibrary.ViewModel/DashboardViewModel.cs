using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.ViewModel
{
    public class DashboardViewModel
    {
        public int NumberOfBooks { get; set; }
        public int NumberCategories { get; set; }
        public int NumberOfUsers { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}
