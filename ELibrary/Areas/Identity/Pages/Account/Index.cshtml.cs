using AutoMapper;
using ELibrary.Core;
using ELibrary.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELibrary.Areas.Identity.Pages.Account;

[Authorize]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public IndexModel(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    [BindProperty]
    public ApplicationUserViewModel Input { get; set; }

    public async Task OnGetAsync()
    {
        var model = await _userManager.GetUserAsync(User);
        Input = _mapper.Map<ApplicationUserViewModel>(model);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
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
}
