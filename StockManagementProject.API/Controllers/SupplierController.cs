using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagementProject.Entities.Entities.Concreate;
using StockManagementProject.Service.Abstract;

namespace StockManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IGenericService<Supplier> _service;

        public SupplierController(IGenericService<Supplier> service)
        {
            this._service = service;
        }
        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        public IActionResult GetActiveSuppliers()
        {
            return Ok(_service.GetActive());
        }
        [HttpPost]
        public IActionResult Add(Supplier supplier)
        {
            return Ok(_service.Create(supplier));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }
        [HttpPut]
        public IActionResult UpdateSupplier(Supplier supplier)
        {
            return Ok(_service.Update(supplier));
        }
        [HttpPut]
        public IActionResult ActivateSupplier(int id)
        {
            return Ok(_service.Activate(id));
        }
        [HttpDelete]
        public IActionResult DeActivateSupplier(int id)
        {
            var sup = _service.GetById(id);
            sup.Status = Entities.Enums.Status.Passive;
            return Ok(_service.Update(sup));
        }
    }
}
