using ELibrary.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ELibrary.Data.Implementation
{
    public class CategoryRepo : CoreRepo<Category>
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Category> dbSet;

        public CategoryRepo(ApplicationDbContext ctx) : base(ctx)
        {
            context = ctx;
            dbSet = context.Set<Category>();
        }

        public Category GetInclude(int id)
        {
            return dbSet.Where(b => b.Id == id && !b.IsDeleted)
                .Include(b => b.Books).FirstOrDefault();
        }

        public IQueryable<Category> GetAllInclude()
        {
            return dbSet.Where(b => !b.IsDeleted).Include(b => b.Books);
        }

        public IQueryable<Category> FindInclude(Expression<Func<Category, bool>> predicate)
        {
            return dbSet.Where(predicate).Where(b => !b.IsDeleted).Include(b => b.Books);
        }
    }
}
