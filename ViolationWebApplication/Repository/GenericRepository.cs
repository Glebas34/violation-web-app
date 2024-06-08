using Microsoft.EntityFrameworkCore;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Data;

namespace ViolationWebApplication.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        protected DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context) 
        {
            _context = context;
            _dbSet=context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity= await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public async Task<IList<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
             _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task ExplicitLoading(T entity, string property)
        {
            await _context.Entry(entity).Reference(property).LoadAsync();
        }

        public async Task ExplicitLoadingCollection(T entity, string property)
        {
            await _context.Entry(entity).Collection(property).LoadAsync();
        }

        public async Task ExplicitLoadingRange(IEnumerable<T> entities, string property)
        {
            foreach(var entity in entities) 
            {
                await _context.Entry(entity).Reference(property).LoadAsync(); 
            }
        }
    }
}
