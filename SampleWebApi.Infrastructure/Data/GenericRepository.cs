using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SampleWebApi.Infrastructure.Data
{
    public interface IGenericRepository<TEntity> where TEntity : DbEntity
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetById(int id);
        Task Insert(TEntity entity);
        Task Delete(int id);
        Task Delete(TEntity entityToDelete);
        Task Update(TEntity entityToUpdate);
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : DbEntity
    {
        internal ApplicationDbContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy == null
                ? query.ToList()
                : orderBy(query).ToList();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Insert(TEntity entity)
        {
            await Task.Run(() =>
            {
                DbSet.Add(entity);
            });
        }

        public virtual async Task Delete(int id)
        {
            var entityToDelete = await DbSet.FindAsync(id);
            await Delete(entityToDelete);
        }

        public virtual async Task Delete(TEntity entityToDelete)
        {
            await Task.Run(() =>
            {
                if (Context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    DbSet.Attach(entityToDelete);
                }

                DbSet.Remove(entityToDelete);
            });
        }

        public virtual async Task Update(TEntity entityToUpdate)
        {
            await Task.Run(() =>
            {
                DbSet.Attach(entityToUpdate);
                Context.Entry(entityToUpdate).State = EntityState.Modified;
            });
        }
    }
}
