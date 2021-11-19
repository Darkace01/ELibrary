namespace ELibrary.Areas.Admin.Controllers;

[Area("admin")]
[Route("admin")]
[Authorize(Roles = AppConstant.AdminRole)]
public class HomeController : Controller
{
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(IMapper mapper, IRepositoryServiceManager repositoryService, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _repositoryService = repositoryService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        DashboardViewModel model = new();
        model.NumberOfBooks = _repositoryService.BookService.GetCount();
        model.NumberCategories = _repositoryService.CategoryService.GetAll().Count();
        var NumberOfUsers = await _userManager.GetUsersInRoleAsync(AppConstant.PublicUserRole);
        model.NumberOfUsers = NumberOfUsers.Count;
        var books = _repositoryService.BookService.GetAllBooks(true).Take(5).ToList();
        model.Books = _mapper.Map<List<BookViewModel>>(books);
        return View(model);
    }
}
