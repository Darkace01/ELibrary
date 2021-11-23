using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELibrary.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin")]
[Authorize(Roles = AppConstant.AdminRole)]
public class BookController : Controller
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

    [Route("books")]
    public IActionResult Index()
    {
        var model = _repositoryService.BookService.GetAllBooks(true).ToList();
        var book = _mapper.Map<List<BookViewModel>>(model);
        return View(book);
    }

    [Route("add-book")]
    public IActionResult AddBook()
    {
        var categories = _repositoryService.CategoryService.GetAll();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }

    [HttpPost("add-book")]
    public async Task<IActionResult> AddBook(AddBookViewModel model)
    {
        try
        {
            if (model.Tags.Count > 0)
            {
                var listOfTags = model.Tags;
                listOfTags = listOfTags.Distinct().ToList();

                var tagsModel = listOfTags.Select(t => new AddTagViewModel() { Name = t, ValidTagName = _repositoryService.TagService.CheckTagName(t) }).ToList();
                var _tags = tagsModel.Where(t => t.ValidTagName).Select(t => new Tag { Name = t.Name, IsFeatured = true });
                model.TagString = string.Join(',', tagsModel.Select(t => t.Name));
                await _repositoryService.TagService.AddRange(_tags);
            }
            var book = _mapper.Map<Book>(model);
            book.Tags = !string.IsNullOrEmpty(model.TagString) ? model.TagString : "";
            book.ImageUrl = model.ImageFile != null ? await _repositoryService.FileStorageService.SaveFile(imageContainer, model.ImageFile) : "";
            if (CommonHelper.CheckFileFormat(model.PdfFile, ".pdf"))
            {
                book.PdfUrl = model.PdfFile == null ? "" : await _repositoryService.FileStorageService.SaveFile(pdfContainer, model.PdfFile);
            }
            else
            {
                ModelState.AddModelError("", "File is not a pdf");
                return View(nameof(AddBook), model);
            }

            await _repositoryService.BookService.Add(book);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            var categories = _repositoryService.CategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(nameof(AddBook), model);
        }
    }

    [Route("edit-book/{id}")]
    public IActionResult EditBook(int id)

    {
        var model = _repositoryService.BookService.GetById(id, true);
        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }
        var book = _mapper.Map<EditBookViewModel>(model);
        book.PreviousImageUrl = model.ImageUrl;
        book.PreviousPdfUrl = model.PdfUrl;
        var categories = _repositoryService.CategoryService.GetAll();
        ViewBag.Categories = categories.ToList();
        return View(book);
    }

    [HttpPost("update-book")]
    public async Task<IActionResult> UpdateBook(EditBookViewModel model)
    {
        try
        {
            if (model.Tags.Count > 0)
            {
                var listOfTags = model.Tags;
                listOfTags = listOfTags.Distinct().ToList();

                var tagsModel = listOfTags.Select(t => new AddTagViewModel() { Name = t, ValidTagName = _repositoryService.TagService.CheckTagName(t) }).ToList();
                var _tags = tagsModel.Where(t => t.ValidTagName).Select(t => new Tag { Name = t.Name, IsFeatured = true });
                model.TagString = string.Join(',', tagsModel.Select(t => t.Name));
                await _repositoryService.TagService.AddRange(_tags);
            }
            var book = _mapper.Map<Book>(model);
            book.Tags = !string.IsNullOrEmpty(model.TagString) ? model.TagString : "";
            book.ImageUrl = model.ImageFile != null
                ? await _repositoryService.FileStorageService.EditFile(imageContainer, model.ImageFile, model.PreviousImageUrl)
                : model.PreviousImageUrl;

            book.PdfUrl = model.PreviousPdfUrl;

            await _repositoryService.BookService.Update(book);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            var categories = _repositoryService.CategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(nameof(EditBook), model);
        }
    }

    [Route("book/delete/{id}")]
    public async Task<IActionResult> DeleteBook(int Id)
    {
        try
        {
            var book = _repositoryService.BookService.GetById(Id);
            if (book == null) return Json(new { error = true, message = "book Not Found" });
            await _repositoryService.BookService.Delete(book);
            return Json(new { error = false, message = "Success" });
        }
        catch (Exception ex)
        {
            return Json(new { error = true, message = ex.Message });
        }
    }

}
