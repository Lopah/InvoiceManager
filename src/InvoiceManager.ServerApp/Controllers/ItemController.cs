using InvoiceManager.Core.Services;
using InvoiceManager.ServerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoiceManager.ServerApp.Controllers
{
    [Route("Invoice/[controller]/{action=Index}")]
    public class ItemController : Controller
    {
        private readonly IInvoiceItemService<InvoiceItemViewModel> _service;

        public ItemController(IInvoiceItemService<InvoiceItemViewModel> service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.ParentId = id;
            return View("Views/Invoice/Item/Index.cshtml", await _service.GetAllForParentid(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteInvoiceItem(id);
            if (result != null)
            {
                return RedirectToAction("Index", "Invoice");
            }
            return null;
        }
        [HttpGet("{id}")]
        public IActionResult Create(int id)
        {
            ViewBag.ParentId = id;
            return View("Views/Invoice/Item/Create.cshtml", new InvoiceItemViewModel {InvoiceId = id});
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Create(int id,[Bind] InvoiceItemViewModel item)
        {
            var result = await _service.CreateInvoiceItem(id, item);
            if (result != null)
            {
                return RedirectToAction("Index", "Item", new { id = id});
            }
            return null;
        }
    }
}
