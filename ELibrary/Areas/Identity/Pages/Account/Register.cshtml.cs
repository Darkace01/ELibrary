// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace ELibrary.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly ILogger<RegisterModel> _logger;
    //private readonly IEmailSender _emailSender;

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore,
        SignInManager<ApplicationUser> signInManager,
        ILogger<RegisterModel> logger, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, IWebHostEnvironment env, IRepositoryServiceManager repositoryService)
    {
        _userManager = userManager;
        _userStore = userStore;
        //_emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        //_emailSender = emailSender;
        _roleManager = roleManager;
        _configuration = configuration;
        _env = env;
        _repositoryService = repositoryService;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     User full name
        /// </summary>
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        /// <summary>
        ///     User Matric No
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Matric Number")]
        public string MatricNo { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }


    public async Task OnGetAsync(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (ModelState.IsValid)
        {
            ApplicationUser user = new()
            {
                Email = Input.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = Input.Email,
                FullName = Input.FullName,
                MatricNo = Input.MatricNo
            };

            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(AppConstant.PublicUserRole))
                    await _roleManager.CreateAsync(new ApplicationRole(AppConstant.PublicUserRole));
                await _userManager.AddToRoleAsync(user, AppConstant.PublicUserRole);

                var emailTemplateBuilder = new EmailTemplateBuilder(_configuration, _env);
                string profileUrl = "https://elibrary.com/company/details";
                var mailBody = emailTemplateBuilder.BuildAccountConfirmationTemplate(Input.FullName, Input.Email, Input.Password, profileUrl);
                await _repositoryService.EmailSender.SendEmailAsync(Input.Email, "Account confirmation", mailBody);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }
}
