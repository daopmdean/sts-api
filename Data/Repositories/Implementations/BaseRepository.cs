using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataContext _context;
        protected DbSet<T> _entities;

        public BaseRepository(DataContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T person)
        {
            _entities.Remove(person);
        }

        public virtual async Task<PagedList<T>> GetAsync(PaginationParams @params)
        {
            return await PagedList<T>
                .CreateAsync(_entities.AsQueryable(), @params.PageNumber, @params.PageSize);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
