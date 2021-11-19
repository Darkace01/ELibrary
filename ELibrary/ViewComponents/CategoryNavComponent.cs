namespace ELibrary.ViewComponents;

[ViewComponent(Name = "CategoryNav")]
public class CategoryNavComponent : ViewComponent
{
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;

    public CategoryNavComponent(IRepositoryServiceManager repositoryService, IMapper mapper)
    {
        _repositoryService = repositoryService;
        _mapper = mapper;
    }

    public IViewComponentResult Invoke()
    {
        var category = _mapper.Map<List<CategoryViewModel>>(_repositoryService.CategoryService.GetAll().ToList());
        return View(category);
    }
}
