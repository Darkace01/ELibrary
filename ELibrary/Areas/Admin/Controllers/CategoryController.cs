﻿namespace ELibrary.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin")]
[Authorize(Roles = AppConstant.AdminRole)]
public class CategoryController : Controller
{
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;
    private readonly string imageContainer = "category";

    public CategoryController(IMapper mapper, IRepositoryServiceManager repositoryService)
    {
        _mapper = mapper;
        _repositoryService = repositoryService;
    }

    [Route("categories")]
    public IActionResult Index()
    {
        var model = _repositoryService.CategoryService.GetAll();
        var categories = _mapper.Map<List<CategoryViewModel>>(model);
        return View(categories);
    }

    [Route("add-category")]
    public IActionResult AddCategory()
    {
        return View();
    }

    [HttpPost("add-category")]
    public async Task<IActionResult> SaveCategory(AddCategoryViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(nameof(AddCategory), model);
            }
            var category = _mapper.Map<Category>(model);
            if (model.ImageFile != null)
            {
                category.ImageUrl = await _repositoryService.FileStorageService.SaveFile(imageContainer, model.ImageFile);
            }
            await _repositoryService.CategoryService.Add(category);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(nameof(AddCategory), model);
        }
    }

    [HttpGet("edit-category/{id}")]
    public IActionResult EditCategory(int id)
    {
        var model = _repositoryService.CategoryService.Get(id);
        if (model == null) return RedirectToAction(nameof(Index));
        var category = _mapper.Map<EditCategoryViewModel>(model);
        category.PreviousImageUrl = model.ImageUrl;
        return View(category);
    }

    [HttpPost("edit-category")]
    public async Task<IActionResult> CategoryUpdate(EditCategoryViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more validations failed");
                return View(nameof(EditCategory), model);
            }
            var category = _mapper.Map<Category>(model);
            category.ImageUrl = model.ImageFile != null
                ? await _repositoryService.FileStorageService.EditFile(imageContainer, model.ImageFile, model.PreviousImageUrl)
                : model.PreviousImageUrl;
            await _repositoryService.CategoryService.Update(category);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(nameof(EditCategory), model);
        }
    }

    [Route("category/delete/{id}")]
    public async Task<IActionResult> DeleteCategory(int Id)
    {
        try
        {
            var category = _repositoryService.CategoryService.Get(Id);
            if (category == null) return Json(new { error = true, message = "Category Not Found" });
            await _repositoryService.CategoryService.Delete(category);
            return Json(new { error = false, message = "Success" });
        }
        catch (Exception ex)
        {
            return Json(new { error = true, message = ex.Message });
        }
    }
}
