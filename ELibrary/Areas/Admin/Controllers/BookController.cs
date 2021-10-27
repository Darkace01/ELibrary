using Microsoft.AspNetCore.Mvc;
using ELibrary.Service.Contract;
using AutoMapper;
using ELibrary.ViewModel;

namespace ELibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class BookController : Controller
    {
        private readonly IRepositoryServiceManager _repositoryService;
        private readonly IMapper _mapper;

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
    }
}
