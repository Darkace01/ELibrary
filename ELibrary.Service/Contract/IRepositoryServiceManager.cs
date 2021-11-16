﻿namespace ELibrary.Service.Contract;

public interface IRepositoryServiceManager
{
    ITagService TagService { get; }
    ICategoryService CategoryService { get; }
    IBookService BookService { get; }
    IFileStorageService FileStorageService { get; }
    IEmailSender EmailSender { get; }
}
