using Microsoft.EntityFrameworkCore;
using WRP3.IServices.Common;
using WRP3.Services.Comman.Exceptions;

namespace WRP3.Services.Comman
{
    public class Service<T> : IService<T> where T : class
    {
        protected readonly DbContext _context;
        internal DbSet<T> _dbSet;
        public Service(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            if (entity == null)
            {
                throw new NotFoundException(nameof(T));
            }

            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            T? entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(T), id);
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> Get(int id)
        {
            T? entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(T), id);
            }

            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<int> Update(int id, T request)
        {
            //T? entity = await _dbSet.FindAsync(id);

            //if (entity == null)
            //{
            //    throw new NotFoundException(nameof(T), id);
            //}

            //_dbSet.Update(entity);
            //await _context.SaveChangesAsync();

            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return id;
        }
    }
}
