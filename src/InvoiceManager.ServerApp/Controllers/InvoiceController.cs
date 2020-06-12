using InvoiceManager.Core.Services;
using InvoiceManager.ServerApp.Models;
using InvoiceManager.ServerApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoiceManager.ServerApp.Controllers
{
    [Route("[controller]/{action=Index}")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService<InvoiceViewModel> _service;

        public InvoiceController(IInvoiceService<InvoiceViewModel> service)
        {
            _service = service;
        }
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _service.GetInvoiceById(id));
        }

        public async Task<IActionResult> Index()
        {
            var results = await _service.GetAllInvoices( );
            return View(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteInvoice(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(InvoiceViewModel invoice)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateInvoice(invoice);
                return RedirectToAction("Index");
            }
            else
                return View(invoice);
        }

        public IActionResult Create()
        {
            return View( );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var existingInvoice = await _service.GetInvoiceById(id);
            if (existingInvoice != null)
            {
                ViewBag.ParentId = id;
                return View("Edit", existingInvoice);
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id,InvoiceViewModel invoice)
        {
            invoice.Id = id;
            var updatedInvoice = await _service.UpdateInvoice(invoice);
            if (updatedInvoice != null)
            {
                return RedirectToAction("Index");
            }
            else
                return BadRequest( );
        }
    }
}
