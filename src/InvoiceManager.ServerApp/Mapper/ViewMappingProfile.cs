using InvoiceManager.Core.Models;
using InvoiceManager.ServerApp.Models;
using InvoiceManager.SqlServer.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.ServerApp.Mapper
{
    public class ViewMappingProfile : MappingProfile
    {
        public ViewMappingProfile()
            : base()
        {
            CreateMap<InvoiceModel, InvoiceViewModel>( )
                .ReverseMap();

            CreateMap<ItemModel, InvoiceItemViewModel>( )
                .ReverseMap( );
        }
    }
}
