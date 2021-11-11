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
