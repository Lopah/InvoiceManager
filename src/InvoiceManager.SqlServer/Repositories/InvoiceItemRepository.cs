using AutoMapper;
using AutoMapper.QueryableExtensions;
using InvoiceManager.Core.Models;
using InvoiceManager.Core.Repositories;
using InvoiceManager.SqlServer.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer.Repositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceItemRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemModel> AddAsync(ItemModel entity)
        {
            var item = _mapper.Map<ItemDto>(entity);
            item.Id = 0;
            await _context.Items.AddAsync(item);

            await _context.SaveChangesAsync( );

            return _mapper.Map<ItemModel>(_context.Items.Where(e => e.Id == item.Id).FirstOrDefault());
        }

        public async Task<ItemModel> DeleteItemById(int id)
        {
            var deletedEntity = await _context.Items.Where(e => e.Id == id).FirstOrDefaultAsync( );

            _context.Items.Remove(deletedEntity);

            await _context.SaveChangesAsync( );

            return _mapper.Map<ItemModel>(deletedEntity);
        }

        public async Task<IEnumerable<ItemModel>> GetAllAsync()
        {
            return await _context.Items
                .ProjectTo<ItemModel>(_mapper.ConfigurationProvider)
                .ToListAsync( );
        }

        public async Task<ItemModel> GetByIdAsync(int id)
        {
            return await _context.Items
                .Where(e => e.Id == id)
                .ProjectTo<ItemModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync( );
        }

        public async Task<ItemModel> GetEntity(ItemModel entity)
        {
            var foundEntity =  await _context.Items
                .FindAsync(_mapper.Map<ItemDto>(entity));

            if (foundEntity != null)
                return _mapper.Map<ItemModel>(foundEntity);
            else
                return null;
        }

        public async Task<ItemModel> RemoveAsync(ItemModel entity)
        {
            var deletedEntity = _context.Items
                .Remove(_mapper.Map<ItemDto>(entity));

            if (deletedEntity.Entity != null)
            {
                await _context.SaveChangesAsync( );
                return _mapper.Map<ItemModel>(deletedEntity.Entity);
            }

            return null;
        }

        public async Task<ItemModel> UpdateAsync(int id, ItemModel updatedEntity)
        {
            var foundEntity = await _context.Items
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync( );

            if (foundEntity != null)
            {
                foundEntity = _mapper.Map<ItemDto>(updatedEntity);

                await _context.SaveChangesAsync( );

                return _mapper.Map<ItemModel>(foundEntity);
            }
            return null;
        }
    }
}
