using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> GetEntity(T entity);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(int id, T updatedEntity);

        Task<T> RemoveAsync(T entity);
    }
}
