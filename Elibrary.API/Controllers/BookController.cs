using Microsoft.AspNetCore.Authorization;

namespace Elibrary.API.Controllers;
[Route("api/book")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;
    private readonly string imageContainer = "ebookimage";
    private readonly string pdfContainer = "ebook";

    public BookController(IRepositoryServiceManager repositoryService, IMapper mapper)
    {
        _repositoryService = repositoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllBooks([FromQuery] bool WithRelationShip = false)
    {
        try
        {
            var _books = _repositoryService.BookService.GetAllBooks(WithRelationShip).ToList();
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status200OK,
                hasError = false,
                data = _mapper.Map<List<BookViewModel>>(_books)
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
            var _book = _repositoryService.BookService.GetById(id, WithRelationShip);
            if (_book == null) return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status404NotFound,
                hasError = false
            });
            return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status200OK,
                hasError = false,
                data = _mapper.Map<BookViewModel>(_book)
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
    [Authorize(Roles = AppConstant.AdminRole)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddBook([FromForm] AddBookViewModel model)
    {
        try
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status200OK, new ApiResponse()
            {
                statusCode = StatusCodes.Status400BadRequest,
                hasError = true,
                message = "One or more validation errors occurred."
            });
            var book = _mapper.Map<Book>(model);
            if (!CommonHelper.CheckFileFormat(model.PdfFile, ".pdf"))
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse()
                {
                    statusCode = StatusCodes.Status400BadRequest,
                    hasError = true,
                    message = "File is not a pdf"
                });
            }
            book.ImageUrl = model.ImageFile != null ? await _repositoryService.FileStorageService.SaveFile(imageContainer, model.ImageFile) : "";
            book.PdfUrl = model.PdfFile == null ? "" : await _repositoryService.FileStorageService.SaveFile(pdfContainer, model.PdfFile);
            if (model.Tags.Count > 0)
            {
                var listOfTags = model.Tags;
                listOfTags = listOfTags.Distinct().ToList();

                var tagsModel = listOfTags.Select(t => new AddTagViewModel() { Name = t, ValidTagName = _repositoryService.TagService.CheckTagName(t) }).ToList();
                var _tags = tagsModel.Where(t => t.ValidTagName).Select(t => new Tag { Name = t.Name, IsFeatured = true });
                model.TagString = string.Join(',', tagsModel.Select(t => t.Name));
                await _repositoryService.TagService.AddRange(_tags);
            }
            book.Tags = !string.IsNullOrEmpty(model.TagString) ? model.TagString : "";
            book.CategoryId = model.CategoryId ?? 1;

            await _repositoryService.BookService.Add(book);
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
}
