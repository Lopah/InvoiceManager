using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.Core.Services
{
    public interface IInvoiceItemService<T> where T : class
    {
        Task<T> CreateInvoiceItem(int parentId, T entity);

        Task<T> DeleteInvoiceItem(int id);

        Task<T> UpdateInvoiceItem(int id, T entity);

        Task<T> GetInvoiceItem(int id, T entity);

        Task<IEnumerable<T>> GetAllForParentid(int id);
    }
}
