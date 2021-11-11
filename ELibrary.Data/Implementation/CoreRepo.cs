using ELibrary.Core;
using ELibrary.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ELibrary.Data.Implementation
{
    public class CoreRepo<TEntity> : ICoreRepo<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Entity> _DbSet;

        public CoreRepo(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._DbSet = this._dbContext.Set<Entity>();
        }

        public void Add(TEntity entity)
        {
            if (entity is Entity)
            {
                entity.DateCreated = DateTime.Now;
                entity.LastModified = DateTime.Now;
                entity.IsDeleted = false;
            }

            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(e => e.DateCreated = DateTime.Now);
            entities.ToList().ForEach(e => e.LastModified = DateTime.Now);
            entities.ToList().ForEach(e => e.IsDeleted = false);
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public TEntity Get(object id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            if (entity == null) return null;
            if (entity.IsDeleted == false)
                return entity;
            return null;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().Where(t => t.IsDeleted == false);
        }

        public IQueryable<TEntity> GetAllWithDelete()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).Where(t => t.IsDeleted == false);
        }

        public int Count()
        {
            return _dbContext.Set<TEntity>().Where(t => t.IsDeleted == false).Count();
        }

        public void Remove(TEntity entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
                Remove(item);
        }

        public void Update(TEntity entity)
        {
            entity.LastModified = DateTime.Now;
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Update(entity);
        }

        public void Attach(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
        }
        public void Include(string entityName)
        {
            _dbContext.Set<TEntity>().Include(entityName);
        }

        public void DeleteFromDb(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteRangeFromDb(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
