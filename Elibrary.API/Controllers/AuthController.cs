namespace Elibrary.API.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;
    private readonly IRepositoryServiceManager _repositoryService;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config, RoleManager<ApplicationRole> roleManager, IWebHostEnvironment env, IRepositoryServiceManager repositoryService)
    {
        _userManager = userManager;
        _config = config;
        _roleManager = roleManager;
        _env = env;
        _repositoryService = repositoryService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var token = GenerateToken(user, userRoles);

                return StatusCode(StatusCodes.Status200OK, new ApiResponse()
                {
                    statusCode = StatusCodes.Status200OK,
                    hasError = false,
                    message = "Authrorized",
                    data = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    }
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status401Unauthorized,
                hasError = true,
                message = "Invalid login credentials"
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status200OK,
                              new ApiResponse() { statusCode = StatusCodes.Status500InternalServerError, hasError = true, message = "An Error has occured please contact adminitrator" });
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        try
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(
                    StatusCodes.Status200OK,
                    new ApiResponse { statusCode = StatusCodes.Status401Unauthorized, hasError = true, message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FullName = model.FullName,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status200OK,
                    new ApiResponse { statusCode = StatusCodes.Status500InternalServerError, hasError = true, message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(AppConstant.PublicUserRole))
                await _roleManager.CreateAsync(new ApplicationRole(AppConstant.PublicUserRole));
            await _userManager.AddToRoleAsync(user, AppConstant.PublicUserRole);

            var emailTemplateBuilder = new EmailTemplateBuilder(_config, _env);
            var profileUrl = "https://elibrary.com/company/details";
            var mailBody = emailTemplateBuilder.BuildAccountConfirmationTemplate(model.FullName, model.Email, model.Password, profileUrl);
            await _repositoryService.EmailSender.SendEmailAsync(model.Email, "Account confirmation", mailBody);
            return StatusCode(StatusCodes.Status200OK,
                              new ApiResponse { statusCode = StatusCodes.Status200OK, hasError = false, message = "User created successfully!" });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status200OK,
                             new ApiResponse() { statusCode = StatusCodes.Status500InternalServerError, hasError = true, message = "An Error has occured please contact adminitrator" });
        }
    }


    #region Helpers
    private JwtSecurityToken GenerateToken(ApplicationUser user, IList<string> userRoles)
    {
        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

        return new JwtSecurityToken(
            issuer: _config["JWT:ValidIssuer"],
            audience: _config["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
    }
    #endregion
}
