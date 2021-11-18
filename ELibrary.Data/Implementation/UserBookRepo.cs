using ELibrary.Core;
using ELibrary.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ELibrary.Data.Implementation;

public class UserBookRepo : CoreRepo<UserBook>, IUserBookRepo
{
    private readonly ApplicationDbContext dbContext;
    private readonly DbSet<UserBook> dbSet;
    public UserBookRepo(ApplicationDbContext ctx) : base(ctx)
    {
        dbContext = ctx;
        dbSet = ctx.Set<UserBook>();
    }

    public UserBook GetInclude(int id)
    {
        return dbSet.Where(c => c.Id == id && !c.IsDeleted)
                    .Include(c => c.Book)
                    .ThenInclude(c => c.Category)
                    .Include(c => c.User)
                    .FirstOrDefault();
    }

    public IQueryable<UserBook> GetAllInclude()
    {
        return dbSet.Where(c => !c.IsDeleted)
                    .Include(c => c.Book)
                    .ThenInclude(c => c.Category)
                    .Include(c => c.User);
    }

    public IQueryable<UserBook> FindInclude(Expression<Func<UserBook, bool>> predicate)
    {
        return dbSet.Where(predicate)
                    .Where(c => !c.IsDeleted)
                    .Include(c => c.Book)
                    .ThenInclude(c => c.Category)
                    .Include(c => c.User);
    }
}
