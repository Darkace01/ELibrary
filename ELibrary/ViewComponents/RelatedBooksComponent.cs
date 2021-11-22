namespace ELibrary.ViewComponents;
[ViewComponent(Name = "RelatedBooks")]
public class RelatedBooksComponent :  ViewComponent
{
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;

    public RelatedBooksComponent(IRepositoryServiceManager repositoryService, IMapper mapper)
    {
        _repositoryService = repositoryService;
        _mapper = mapper;
    }

    public IViewComponentResult Invoke(int bookId)
    {
        var relatedBooks = _mapper.Map<List<BookViewModel>>(_repositoryService.BookService.GetAllBooks(true).Where(x => x.Id != bookId).Take(6).OrderBy(x => x.Name).ToList());
        return View(relatedBooks);
    }
}
