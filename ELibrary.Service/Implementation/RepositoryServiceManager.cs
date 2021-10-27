using ELibrary.Data.Contract;
using ELibrary.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Service.Implementation
{
    public class RepositoryServiceManager : IRepositoryServiceManager
    {
        private readonly IUnitOfWork uow;
        private ITagService _tagService;
        private ICategoryService _categoryService;
        private IBookService _bookService;

        public RepositoryServiceManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }



        public ITagService TagService
        {
            get
            {
                if (_tagService == null)
                    _tagService = new TagService(uow);

                return _tagService;
            }
        }
        
        public ICategoryService CategoryService
        {
            get
            {
                if (_categoryService == null)
                    _categoryService = new CategoryService(uow);

                return _categoryService;
            }
        }
        
        public IBookService BookService
        {
            get
            {
                if (_bookService == null)
                    _bookService = new BookService(uow);

                return _bookService;
            }
        }
    }
}
