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
            CreateMap<ItemDto, ItemModel>()
                .ForMember(m => m.Price,
                opts => opts.MapFrom(source => source.Value))
                .ReverseMap( )
                .ForMember(m => m.Value,
                opts => opts.MapFrom(source => source.Price));

            CreateMap<InvoiceDto, InvoiceModel>( )
                .ForMember(m => m.Items,
                opts => opts.MapFrom(source => source.InvoiceItems))
                .ReverseMap( )
                .ForMember(m => m.InvoiceItems,
                opts => opts.MapFrom(source => source.Items));
        }
    }
}
