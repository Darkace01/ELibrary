using ELibrary.Core;
using ELibrary.Data.Contract;
using ELibrary.Data.Implementation;
using ELibrary.Service.Contract;

namespace ELibrary.Service.Implementation;

public class UserBookService : IUserBookService
{
    private readonly UnitOfWork _uow;
    public UserBookService(IUnitOfWork uow)
    {
        _uow = uow as UnitOfWork;
    }
    public async Task Add(UserBook userBook)
    {
        if (!CheckIfUserOwnsBbook(userBook.UserId, userBook.BookId))
        {
            _uow.UserBookRepo.Add(userBook);
            await _uow.Save();
        }
    }


    public UserBook Get(int id, bool includeRelationships = false)
    {
        if (includeRelationships)
            return _uow.UserBookRepo.GetInclude(id);
        else
            return _uow.UserBookRepo.Get(id);
    }

    public IQueryable<UserBook> GetAll(bool includeRelationships)
    {
        if (includeRelationships)
            return _uow.UserBookRepo.GetAllInclude();
        else
            return _uow.UserBookRepo.GetAll();
    }

    public async Task Delete(UserBook userBook)
    {
        _uow.UserBookRepo.Remove(userBook);
        await _uow.Save();
    }
    #region Private Helpers
    private bool CheckIfUserOwnsBbook(string userId, int bookId)
    {
        var userBook = _uow.UserBookRepo.GetAll().FirstOrDefault(u => u.UserId == userId && u.BookId == bookId);
        if (userBook != null)
            return true;

        return false;
    }
    #endregion
}
