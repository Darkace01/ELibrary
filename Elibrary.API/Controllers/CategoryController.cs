using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Elibrary.API.Controllers;
[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;
    private readonly string imageContainer = "category";

    public CategoryController(IMapper mapper, IRepositoryServiceManager repositoryService)
    {
        _mapper = mapper;
        _repositoryService = repositoryService;
    }

    [HttpGet]
    public IActionResult GetAllCategories()
    {
        try
        {
            var model = _repositoryService.CategoryService.GetAll();
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status200OK,
                hasError = false,
                data = _mapper.Map<List<CategoryViewModel>>(model)
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status500InternalServerError,
                hasError = true,
                message = "An Error has occured please contact adminitrator"
            });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id, [FromQuery] bool WithRelationShip = false)
    {
        try
        {
            var category = _repositoryService.CategoryService.Get(id, WithRelationShip);
            if (category == null) return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status404NotFound,
                hasError = true,
                message = "Category not found"
            });
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status200OK,
                hasError = false,
                data = _mapper.Map<CategoryViewModel>(category)
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status500InternalServerError,
                hasError = true,
                message = "An Error has occured please contact adminitrator"
            });
        }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
    {
        try
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status400BadRequest,
                hasError = true,
                message = "Provide Required Fields"
            });
            var category = _mapper.Map<Category>(model);
            if (model.ImageFile != null)
            {
                category.ImageUrl = await _repositoryService.FileStorageService.SaveFile(imageContainer, model.ImageFile);
            }
            await _repositoryService.CategoryService.Add(category);

            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status200OK,
                hasError = false,
                message = "Saved"
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status500InternalServerError,
                hasError = true,
                message = "An Error has occured please contact adminitrator"
            });
        }
    }

    [HttpPost("update/{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CategoryUpdate(EditCategoryViewModel model)
    {
        try
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status400BadRequest,
                hasError = true,
                message = "Provide Required Fields"
            });

            var category = _mapper.Map<Category>(model);

            category.ImageUrl = model.ImageFile != null
                ? await _repositoryService.FileStorageService.EditFile(imageContainer, model.ImageFile, model.PreviousImageUrl)
                : model.PreviousImageUrl;
            await _repositoryService.CategoryService.Update(category);

            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status200OK,
                hasError = false,
                message = "Updated"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status500InternalServerError,
                hasError = true,
                message = "An Error has occured please contact adminitrator"
            });
        }
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteCategory(int Id)
    {
        try
        {
            var category = _repositoryService.CategoryService.Get(Id);
            if (category == null) return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status404NotFound,
                hasError = true,
                message = "Category not found"
            });
            await _repositoryService.CategoryService.Delete(category);

            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status200OK,
                hasError = false,
                message = "Deleted"
            });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status500InternalServerError,
                hasError = true,
                message = "An Error has occured please contact adminitrator"
            });
        }
    }
}
