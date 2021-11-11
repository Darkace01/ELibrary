using ELibrary.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ELibrary.Data.Implementation
{
    public class BookRepo : CoreRepo<Book>
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Book> dbSet;

        public BookRepo(ApplicationDbContext ctx) : base(ctx)
        {
            context = ctx;
            dbSet = context.Set<Book>();
        }

        public Book GetInclude(int id)
        {
            return dbSet.Where(c => c.Id == id && !c.IsDeleted)
                //.Include(c => c.Tags)
                .Include(c => c.Category)
                .FirstOrDefault();
        }

        public IQueryable<Book> GetAllInclude()
        {
            return dbSet.Where(c => !c.IsDeleted)
                //.Include(c => c.Tags)
                .Include(c => c.Category);
        }

        public IQueryable<Book> FindInclude(Expression<Func<Book, bool>> predicate)
        {
            return dbSet.Where(predicate).Where(c => !c.IsDeleted)
                //.Include(c => c.Tags)
                .Include(c => c.Category);
        }
    }
}
