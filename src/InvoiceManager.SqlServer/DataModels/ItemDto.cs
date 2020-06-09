using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer.DataModels
{
    public class ItemDto
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public int InvoiceId { get; set; }

        public InvoiceDto Invoice { get; set; }
    }
}
