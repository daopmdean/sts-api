using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagedList<T>> GetAsync(PaginationParams @params);
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
    }
}
