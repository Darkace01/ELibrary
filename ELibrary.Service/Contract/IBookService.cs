namespace ELibrary.Service.Contract;

public interface IBookService
{
    Task Add(Book book);
    Task Delete(Book book);
    IQueryable<Book> GetAllBooks(bool includeRelationship = false);
    IQueryable<Book> GetAllBooksInCategory(int id);
    string GetBookTags(int id);
    Book GetById(int id, bool includeRelationship = false);
    int GetCount();
    Task Update(Book book);
    Task UpdatetBasicInfo(Book book, Category category);
}
