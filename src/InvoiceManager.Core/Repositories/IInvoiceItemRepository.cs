using InvoiceManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.Core.Repositories
{
    public interface IInvoiceItemRepository : IGenericRepository<ItemModel>
    {
        Task<ItemModel> DeleteItemById(int id);
    }
}
