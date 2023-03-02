using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace OnlineShop.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> q = _context.Set<T>();
            q = includeProperties.Aggregate(q, (current, includeProperties) => current.Include(includeProperties));
            return await q.ToListAsync();
        }


        public async Task AddAsync(T entity)
        {

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var model = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return model;
        }
        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperty)
        {
            IQueryable<T> q = _context.Set<T>();
            q = includeProperty.Aggregate(q, (current, includeProperty) => current.Include(includeProperty));
            return await q.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entry = _context.Entry<T>(entity);
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entry = _context.Entry<T>(entity);
            entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
