using Microsoft.AspNetCore.Mvc;
using ELibrary.Service.Contract;
using AutoMapper;
using ELibrary.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ELibrary.Core;

namespace ELibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class BookController : Controller
    {
        private readonly IRepositoryServiceManager _repositoryService;
        private readonly IMapper _mapper;
        private string container = "ebook";
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
                if (!string.IsNullOrEmpty(model.Tags))
                {
                    model.Tags = model.Tags.ToLower();
                    var listOfTags = model.Tags.Split(',').ToList();
                    listOfTags = listOfTags.Distinct().ToList();

                    var tagsModel = listOfTags.Select(t => new AddTagViewModel() { Name = t, ValidTagName = _repositoryService.TagService.CheckTagName(t) }).ToList();
                    var _tags = tagsModel.Where(t => t.ValidTagName).Select(t => new Tag { Name = t.Name, IsFeatured = true });
                    model.Tags = string.Join(',', tagsModel.Select(t => t.Name));
                    await _repositoryService.TagService.AddRange(_tags);
                }
                var book = _mapper.Map<Book>(model);
                if (model.ImageFile != null)
                {
                    book.ImageUrl = await _repositoryService.FileStorageService.SaveFile(container, model.ImageFile);
                }
                await _repositoryService.BookService.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(nameof(AddBook), model);
            }
        }

        [Route("edit-book/{id}")]
        public IActionResult EditBook(int id)
        {
            var model = _repositoryService.BookService.GetById(id, true);
            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var book = _mapper.Map<EditBookViewModel>(model);
            var categories = _repositoryService.CategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(book);
        }

    }
}
