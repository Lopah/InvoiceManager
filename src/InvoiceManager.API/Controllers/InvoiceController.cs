using InvoiceManager.Core.Models;
using InvoiceManager.Core.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [HttpGet("Unpaid")]
        public async Task<IActionResult> GetUnpaid()
        {
            var results = await _invoiceRepository.GetUnpaidInvoices( );

            return results.Any( ) ? Ok(results) : (IActionResult)NotFound( );
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _invoiceRepository.GetByIdAsync(id);

            return result != null ? Ok(result) : (IActionResult)NotFound( );
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PayInvoice(int id)
        {
            var foundEntity = await _invoiceRepository.GetUnpaidInvoiceByIdAsync(id);

            return foundEntity != null ? Ok(await _invoiceRepository.PayInvoiceAsync(foundEntity)) : (IActionResult)NotFound( );
        }

        [HttpPatch("edit/{id}")]
        public async Task<IActionResult> EditInvoice(
            int id,
            [FromBody] JsonPatchDocument<InvoiceModel> patchDocument)
        {
            if (patchDocument != null)
            {
                var invoice = await _invoiceRepository.GetByIdAsync(id);

                patchDocument.ApplyTo(invoice);

                return Ok(await _invoiceRepository.UpdateAsync(id,invoice));
            }
            else
            {
                return NotFound( );
            }
        }
    }
}
