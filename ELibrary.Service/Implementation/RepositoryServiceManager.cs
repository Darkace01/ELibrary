using ELibrary.Data.Contract;
using ELibrary.Service.Contract;
using Microsoft.Extensions.Configuration;

namespace ELibrary.Service.Implementation;

public class RepositoryServiceManager : IRepositoryServiceManager
{
    private readonly IUnitOfWork uow;
    private ITagService _tagService;
    private ICategoryService _categoryService;
    private IBookService _bookService;
    private IFileStorageService _fileStorageService;
    private IEmailSender _emailSender;
    private readonly IConfiguration config;

    public RepositoryServiceManager(IUnitOfWork uow, IConfiguration config)
    {
        this.uow = uow;
        this.config = config;
    }



    public ITagService TagService {
        get {
            if (_tagService == null)
                _tagService = new TagService(uow);

            return _tagService;
        }
    }

    public ICategoryService CategoryService {
        get {
            if (_categoryService == null)
                _categoryService = new CategoryService(uow);

            return _categoryService;
        }
    }

    public IBookService BookService {
        get {
            if (_bookService == null)
                _bookService = new BookService(uow);

            return _bookService;
        }
    }

    public IFileStorageService FileStorageService {
        get {
            if (_fileStorageService == null)
                _fileStorageService = new FileStorageService(config);

            return _fileStorageService;
        }
    }

    public IEmailSender EmailSender {
        get {
            if (_emailSender == null)
                _emailSender = new EmailSender(config);

            return _emailSender;
        }
    }
}
