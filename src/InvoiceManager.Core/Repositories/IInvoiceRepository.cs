using InvoiceManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.Core.Repositories
{
    public interface IInvoiceRepository : IGenericRepository<InvoiceModel>
    {
        Task<InvoiceModel> AddInvoiceItemAsync(InvoiceModel invoice, ItemModel item);

        Task<InvoiceModel> RemoveInvoiceItemAsync(InvoiceModel invoice, ItemModel item);

        Task<IEnumerable<InvoiceModel>> GetUnpaidInvoices();

        Task<InvoiceModel> PayInvoiceAsync(InvoiceModel invoice);
    }
}
