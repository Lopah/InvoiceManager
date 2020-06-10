using AutoMapper;
using InvoiceManager.Core.Models;
using InvoiceManager.SqlServer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemDto, ItemModel>( )
                .ReverseMap( );

            CreateMap<InvoiceDto, InvoiceModel>( )
                .ReverseMap( );
        }
    }
}
