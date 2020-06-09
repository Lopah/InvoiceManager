using InvoiceManager.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [Route("/unpaid")]
        public async Task<IActionResult> GetUnpaid()
        {
            var results = await _invoiceRepository.GetUnpaidInvoices( );

            return results.Any( ) ? Ok(results) : (IActionResult)NotFound( );
        }

        [HttpPost]
        [Route("/{id}")]
        public async Task<IActionResult> PayInvoice(int id)
        {
            var foundEntity = await _invoiceRepository.GetByIdAsync(id);

            foundEntity != null ? _invoiceRepository.PayInvoiceAsync(foundEntity) : NotFound( );
        }
    }
}
