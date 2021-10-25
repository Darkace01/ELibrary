using ELibrary.Core;
using ELibrary.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data.Implementation
{
    public class TagRepo : CoreRepo<Tag>, ITagRepo
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<Tag> dbSet;
        public TagRepo(ApplicationDbContext ctx) : base(ctx)
        {
            dbContext = ctx;
            dbSet = ctx.Set<Tag>();
        }
    }
}
