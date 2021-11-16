using AutoMapper;
using ELibrary.Models;
using ELibrary.Service.Contract;
using ELibrary.Utility;
using ELibrary.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ELibrary.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepositoryServiceManager _repositoryService;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IRepositoryServiceManager repositoryService, IMapper mapper)
    {
        _logger = logger;
        _repositoryService = repositoryService;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var books = _repositoryService.BookService.GetAllBooks(true).ToList();
        var category = _repositoryService.CategoryService.GetAll().ToList();
        HomeViewModel model = new();
        model.Books = _mapper.Map<List<BookViewModel>>(books);
        model.Categories = _mapper.Map<List<CategoryViewModel>>(category);
        return View(model);
    }

    [Route("{id}")]
    public IActionResult Detail(int id)
    {
        var model = _repositoryService.BookService.GetById(id, true);
        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }
        var book = _mapper.Map<BookViewModel>(model);
        return View(book);
    }

    [Route("library")]
    public IActionResult Listing(string query, int categoryId, int pagenumber, string orderBy, int pagesize = AppConstant.PageSize)
    {
        query = string.IsNullOrEmpty(query) ? "" : query.ToLower();
        pagesize = pagesize < 1 ? 1 : pagesize;
        pagenumber = pagenumber < 1 ? 1 : pagenumber;

        var books = _repositoryService.BookService.GetAllBooks(true)
            .Where(x => x.Name.Contains(query)
            || x.Author.Contains(query)
            || x.Category.Name.Contains(query)
            || x.Tags.Contains(query)
            || x.Code.Contains(query)
            );
        if (categoryId != 0)
        {
            books = books.Where(c => c.CategoryId == categoryId);
        }


        var pageTotal = Convert.ToInt32(Math.Ceiling((double)books.Count() / pagesize));
        books = books.Skip((pagenumber - 1) * pagesize).Take(pagesize);
        if (!string.IsNullOrEmpty(orderBy))
        {
            switch (orderBy)
            {
                case "Naz":
                    books = books.OrderBy(x => x.Name);
                    break;
                case "Nza":
                    books = books.OrderByDescending(x => x.Name);
                    break;
                case "Caz":
                    books = books.OrderBy(x => x.Category.Name);
                    break;
                case "Cza":
                    books = books.OrderByDescending(x => x.Category.Name);
                    break;
                default:
                    break;
            }
        }

        var category = _repositoryService.CategoryService.GetAll(true).ToList();
        ListingViewModel model = new();
        model.Books = _mapper.Map<List<BookViewModel>>(books.ToList());
        model.Categories = _mapper.Map<List<CategoryViewModel>>(category);
        model.PageTotal = Convert.ToInt32(Math.Ceiling((double)books.Count() / pagesize));
        model.PageNumber = pagenumber;
        model.CurrentPage = pagenumber;
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
