namespace ELibrary.Areas.Identity.Pages.Account;

[Authorize]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;

    public IndexModel(UserManager<ApplicationUser> userManager, IMapper mapper, IRepositoryServiceManager repositoryService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _repositoryService = repositoryService;
    }

    [BindProperty]
    public ApplicationUserViewModel Input { get; set; }
    [BindProperty]
    public List<UserBookViewModel> UserBooks { get; set; }

    public async Task OnGetAsync()
    {
        var user = await GetCurrentUser();
        var userModel = await _userManager.GetUserAsync(User);
        var userBooks = _repositoryService.UserBookService.GetAll(true).Where(x => x.UserId == user.Id);
        Input = _mapper.Map<ApplicationUserViewModel>(userModel);
        UserBooks = _mapper.Map<List<UserBookViewModel>>(userBooks);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var user = await GetCurrentUser();
            user.FullName = Input.FullName;
            await _userManager.UpdateAsync(user);
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }

    private async Task<ApplicationUser> GetCurrentUser()
    {
        return await _userManager.GetUserAsync(User);
    }
}
