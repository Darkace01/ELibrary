using ELibrary.Core;

namespace ELibrary.Service.Contract;

public interface IUserBookService
{
    Task Add(UserBook userBook);
    Task Delete(UserBook userBook);
    UserBook Get(int id, bool includeRelationships = false);
    IQueryable<UserBook> GetAll(bool includeRelationships);
}
