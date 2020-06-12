using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.ServerApp.Models
{
    public class InvoiceViewModel
    {
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }

        [Display(Description = "If an invoice is paid for")]
        public bool Paid { get; set; }

        public ICollection<InvoiceItemViewModel> Items { get; set; }
    }
}
