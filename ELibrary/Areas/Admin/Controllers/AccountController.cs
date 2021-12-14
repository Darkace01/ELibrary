namespace ELibrary.Areas.Admin.Controllers;

[Area("admin")]
[Route("admin")]
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly IRepositoryServiceManager _repositoryService;

    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUserStore<ApplicationUser> userStore, IConfiguration configuration, IWebHostEnvironment env, IRepositoryServiceManager repositoryService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _userStore = userStore;
        _configuration = configuration;
        _env = env;
        _repositoryService = repositoryService;
    }

    [Route("sign-in")]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost("sign-in-post")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignInPost(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.Username);
            }
            if (user != null)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, AppConstant.AdminRole);
                if (isAdmin)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe ?? false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
            }

        }
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View("SignIn", model);
    }

    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register-post")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterPost(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FullName = model.FullName,
            };

            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(AppConstant.AdminRole))
                    await _roleManager.CreateAsync(new ApplicationRole(AppConstant.AdminRole));
                await _userManager.AddToRoleAsync(user, AppConstant.AdminRole);

                var emailTemplateBuilder = new EmailTemplateBuilder(_configuration, _env);
                string profileUrl = "https://elibrary.com/company/details";
                var mailBody = emailTemplateBuilder.BuildAccountConfirmationTemplate(model.FullName, model.Email, model.Password, profileUrl);
                await _repositoryService.EmailSender.SendEmailAsync(model.Email, "Account confirmation", mailBody);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home", new { area = "admin" });

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        ModelState.AddModelError(string.Empty, "Check your input");
        return View("Register", model);
    }
}
