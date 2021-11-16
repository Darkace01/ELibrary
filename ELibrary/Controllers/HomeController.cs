using AutoMapper;
using ELibrary.Models;
using ELibrary.Service.Contract;
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
        var _books = _mapper.Map<List<BookViewModel>>(books);
        HomeViewModel model = new();
        model.books = _books;
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
