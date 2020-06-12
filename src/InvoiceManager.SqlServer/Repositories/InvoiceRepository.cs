using AutoMapper;
using AutoMapper.QueryableExtensions;
using InvoiceManager.Core.Models;
using InvoiceManager.Core.Repositories;
using InvoiceManager.SqlServer.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            var mappedInvoice = await _context.Invoices
                .Where(e => e.Id == id)
                .Include(e => e.InvoiceItems)
                .FirstOrDefaultAsync( );
            return _mapper.Map<InvoiceModel>(mappedInvoice);
        }

        public async Task<InvoiceModel> GetEntity(InvoiceModel entity)
        {
            var sourceInvoice = await _context.Invoices
                .Where(e => e.Id == entity.Id)
                .Include(e => e.InvoiceItems)
                .FirstOrDefaultAsync( );

            return _mapper.Map<InvoiceModel>(sourceInvoice);
        }

        public async Task<InvoiceModel> GetUnpaidInvoiceByIdAsync(int id)
        {
            var sourceInvoice = await _context.Invoices
                .Where(e => e.Id == id && e.Paid != true)
                .Include(e => e.InvoiceItems)
                .FirstOrDefaultAsync( );

            return _mapper.Map<InvoiceModel>(sourceInvoice);
        }

        public async Task<IEnumerable<InvoiceModel>> GetUnpaidInvoices()
        {
            var invoices = await _context.Invoices
                .Where(e => e.Paid != true)
                .Include(e => e.InvoiceItems)
                .ToListAsync( );

            return _mapper.ProjectTo<InvoiceModel>(invoices.AsQueryable());
        }

        public async Task<InvoiceModel> PayInvoiceAsync(InvoiceModel invoice)
        {
            var sourceInvoice = await _context.Invoices
                .Where(e => e.Id == invoice.Id)
                .Include(e => e.InvoiceItems)
                .FirstOrDefaultAsync( );

            sourceInvoice.Paid = true;

            await _context.SaveChangesAsync( );

            return _mapper.Map<InvoiceModel>(await this.GetEntity(invoice));
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
