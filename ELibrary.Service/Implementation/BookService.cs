namespace ELibrary.Service.Implementation;

public class BookService : IBookService
{
    private readonly UnitOfWork _uow;

    public BookService(IUnitOfWork uow)
    {
        _uow = uow as UnitOfWork;
    }

    public async Task Add(Book book)
    {
        _uow.BookRepo.Add(book);
        await _uow.Save();
    }

    public IQueryable<Book> GetAllBooks(bool includeRelationship = false)
    {
        if (!includeRelationship)
            return _uow.BookRepo.GetAll();
        else
            return _uow.BookRepo.GetAllInclude();
    }

    public IQueryable<Book> GetAllBooksInCategory(int id)
    {
        return _uow.BookRepo.FindInclude(b => b.CategoryId == id);
    }

    public string GetBookTags(int id)
    {
        var book = _uow.BookRepo.Get(id);
        if (book == null)
            return "";
        return book.Tags;
    }

    public Book GetById(int id, bool includeRelationship = false)
    {
        if (!includeRelationship)
            return _uow.BookRepo.Get(id);
        else
            return _uow.BookRepo.GetInclude(id);
    }

    public int GetCount()
    {
        return _uow.BookRepo.Count();
    }

    public async Task Update(Book book)
    {
        _uow.BookRepo.Update(book);
        await _uow.Save();
    }

    public async Task UpdatetBasicInfo(Book book, Category category)
    {
        book.Category = category;

        await Update(book);
    }

    public async Task Delete(Book book)
    {
        _uow.BookRepo.Remove(book);
        await _uow.Save();
    }
}
