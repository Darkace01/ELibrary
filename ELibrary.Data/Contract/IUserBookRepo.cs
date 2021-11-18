using ELibrary.Core;
using System.Linq.Expressions;

namespace ELibrary.Data.Contract;

public interface IUserBookRepo : ICoreRepo<UserBook>
{
    IQueryable<UserBook> FindInclude(Expression<Func<UserBook, bool>> predicate);
    IQueryable<UserBook> GetAllInclude();
    UserBook GetInclude(int id);
}
