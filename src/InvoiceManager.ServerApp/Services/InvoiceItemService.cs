using AutoMapper;
using InvoiceManager.Core.Models;
using InvoiceManager.Core.Repositories;
using InvoiceManager.Core.Services;
using InvoiceManager.ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.ServerApp.Services
{
    public class InvoiceItemService : IInvoiceItemService<InvoiceItemViewModel>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IMapper _mapper;

        public InvoiceItemService(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _mapper = mapper;
        }

        public async Task<InvoiceItemViewModel> CreateInvoiceItem(int parentId, InvoiceItemViewModel entity)
        {
            entity.InvoiceId = parentId;
            var createdEntity = await _invoiceItemRepository.AddAsync(_mapper.Map<ItemModel>(entity));

            if (createdEntity == null)
            {
                return null;
            }
            else
                return _mapper.Map<InvoiceItemViewModel>(createdEntity);
        }

        public async Task<InvoiceItemViewModel> DeleteInvoiceItem(int itemId)
        {
            var deletedEntity = await _invoiceItemRepository.DeleteItemById(itemId);

            if (deletedEntity != null)
            {
                return _mapper.Map<InvoiceItemViewModel>(deletedEntity);
            }
            else
                return null;
        }

        public async Task<IEnumerable<InvoiceItemViewModel>> GetAllForParentid(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);

            return _mapper.ProjectTo<InvoiceItemViewModel>(invoice.Items.AsQueryable());
        }

        public Task<InvoiceItemViewModel> GetInvoiceItem(int id, InvoiceItemViewModel entity)
        {
            throw new NotImplementedException( );
        }

        public Task<InvoiceItemViewModel> UpdateInvoiceItem(int id, InvoiceItemViewModel entity)
        {
            throw new NotImplementedException( );
        }
    }
}
