using AutoMapper;
using InvoiceManager.Core.Models;
using InvoiceManager.Core.Repositories;
using InvoiceManager.SqlServer.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InvoiceModel> AddAsync(InvoiceModel entity)
        {
            var sourceInvoice = _mapper.Map<InvoiceDto>(entity);

            await _context.Invoices.AddAsync(sourceInvoice);

            // save immediately
            await _context.SaveChangesAsync( );
            // find in the db and return it.
            return _mapper.Map<InvoiceModel>(_context.Invoices.Find(sourceInvoice));
        }

        public async Task<InvoiceModel> AddInvoiceItemAsync(InvoiceModel invoice, ItemModel item)
        {
            var sourceInvoice = _mapper.Map<InvoiceDto>(invoice);

            sourceInvoice.InvoiceItems.Add(_mapper.Map<ItemDto>(item));

            await _context.SaveChangesAsync( );

            return _mapper.Map<InvoiceModel>(_context.Invoices.Find(sourceInvoice));

        }

        public Task<IEnumerable<InvoiceModel>> GetAllAsync()
        {
            throw new NotImplementedException( );
        }

        public async Task<InvoiceModel> GetByIdAsync(int id)
        {
            var mappedInvoice = await _context.Invoices.Where(e => e.Id == id).FirstOrDefaultAsync( );
            return _mapper.Map<InvoiceModel>(mappedInvoice);
        }

        public Task<InvoiceModel> GetEntity(InvoiceModel entity)
        {
            var sourceInvoice = _mapper.Map<InvoiceDto>(entity);

            return Task.FromResult(_mapper.Map<InvoiceModel>(_context.Invoices.Find(sourceInvoice)));
        }

        public async Task<IEnumerable<InvoiceModel>> GetUnpaidInvoices()
        {
            return await _mapper.ProjectTo<InvoiceModel>(_context.Invoices).ToListAsync();
        }

        public async Task<InvoiceModel> PayInvoiceAsync(InvoiceModel invoice)
        {
            var sourceInvoice = _mapper.Map<InvoiceDto>(invoice);

            sourceInvoice.Paid = true;

            await _context.SaveChangesAsync( );

            return _mapper.Map<InvoiceModel>(_context.Invoices
                .Where(e => e.Id == sourceInvoice.Id)
                .FirstOrDefault( ));
        }

        public async Task<InvoiceModel> RemoveAsync(InvoiceModel entity)
        {
            var sourceInvoice = _mapper.Map<InvoiceDto>(entity);

            _context.Invoices.Remove(sourceInvoice);

            await _context.SaveChangesAsync( );

            return entity;
        }

        public async Task<InvoiceModel> RemoveInvoiceItemAsync(InvoiceModel invoice, ItemModel item)
        {
            var sourceInvoice = _mapper.Map<InvoiceDto>(invoice);

            var partialDbInvoice = _context.Invoices.Find(sourceInvoice);

            var completeDbInvoice = await _context.Invoices.Where(e => e.Id == partialDbInvoice.Id)
                .Include(x => x.InvoiceItems).FirstOrDefaultAsync();

            completeDbInvoice.InvoiceItems.Remove(_mapper.Map<ItemDto>(item));

            await _context.SaveChangesAsync( );

            return _mapper.Map<InvoiceModel>(_context.Invoices.Find(completeDbInvoice));
        }

        public async Task<InvoiceModel> UpdateAsync(int id, InvoiceModel updatedEntity)
        {
            var sourceEntity = await _context.Invoices.Where(x => x.Id == id)
                .Include(e => e.InvoiceItems)
                .FirstOrDefaultAsync();

            sourceEntity = _mapper.Map<InvoiceDto>(updatedEntity);

            await _context.SaveChangesAsync( );


            return _mapper.Map<InvoiceModel>(_context.Invoices.Where(x => x.Id == id)
                .Include(e => e.InvoiceItems).First( ));
        }
    }
}
