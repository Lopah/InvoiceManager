using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.Core.Services
{
    public interface IInvoiceService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllInvoices();

        Task<T> DeleteInvoice(int id);

        Task<T> CreateInvoice(T entity);

        Task<T> GetInvoiceById(int id);

        Task<T> UpdateInvoice(T entity);
    }
}
