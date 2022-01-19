namespace Elibrary.API.Controllers;
[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;

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
}
