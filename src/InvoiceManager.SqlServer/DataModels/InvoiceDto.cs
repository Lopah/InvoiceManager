using InvoiceManager.SqlServer.DataModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer.DataModels
{
    public class InvoiceDto : BaseDto
    { 
        public bool Paid { get; set; }

        public ICollection<ItemDto> InvoiceItems { get; set; }
    }
}
