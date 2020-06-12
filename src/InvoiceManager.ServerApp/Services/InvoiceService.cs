using AutoMapper;
using InvoiceManager.Core.Models;
using InvoiceManager.Core.Repositories;
using InvoiceManager.Core.Services;
using InvoiceManager.ServerApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.ServerApp.Services
{
    public class InvoiceService : IInvoiceService<InvoiceViewModel>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<InvoiceViewModel> CreateInvoice(InvoiceViewModel model)
        {

            var result = await _repository.AddAsync(_mapper.Map<InvoiceModel>(model));

            if (result != null)
            {
                return _mapper.Map<InvoiceViewModel>(result);
            }
            return null;
        }

        public async Task<InvoiceViewModel> DeleteInvoice(int id)
        {
            var sourceInvoice = await _repository.GetByIdAsync(id);

            if (sourceInvoice != null)
            {
                await _repository.RemoveAsync(sourceInvoice);

                return _mapper.Map<InvoiceViewModel>(sourceInvoice);
            }
            return null;
        }

        public async Task<IEnumerable<InvoiceViewModel>> GetAllInvoices()
        {
            var invoices = await _repository.GetAllAsync( );

            return _mapper.ProjectTo<InvoiceViewModel>(invoices.AsQueryable( ));
        }

        public async Task<InvoiceViewModel> GetInvoiceById(int id)
        {
            var invoice = await _repository.GetByIdAsync(id);

            return _mapper.Map<InvoiceViewModel>(invoice);
        }

        public async Task<InvoiceViewModel> UpdateInvoice(InvoiceViewModel entity)
        {
            // this is a workaround, because I don't know how else do I get the same properties in order to map correctly.
            var test = await _repository.GetByIdAsync(entity.Id);
            entity.Items = test.Items.Select(x => _mapper.Map<InvoiceItemViewModel>(x)).ToList();
            //


            var updatedEntity = await _repository.UpdateAsync(entity.Id, _mapper.Map<InvoiceModel>(entity));

            if (updatedEntity != null)
            {
                return _mapper.Map<InvoiceViewModel>(updatedEntity);
            }
            return null;
        }
    }
}
