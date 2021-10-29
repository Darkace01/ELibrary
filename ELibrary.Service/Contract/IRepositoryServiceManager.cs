using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Service.Contract
{
    public interface IRepositoryServiceManager
    {
        ITagService TagService { get; }
        ICategoryService CategoryService { get; }
        IBookService BookService { get; }
        IFileStorageService FileStorageService { get; }
        IEmailSender EmailSender { get; }
    }
}
