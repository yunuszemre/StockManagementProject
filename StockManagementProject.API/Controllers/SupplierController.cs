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
        private readonly IGenericService<OrderDetails> _orderDetailService;
        private readonly IGenericService<Order> _orderService;
        public SupplierController(IGenericService<Supplier> service, IGenericService<Order> orderService, IGenericService<OrderDetails> orderDetailService)
        {
            this._orderService= orderService;
            this._service = service;
            this._orderDetailService = orderDetailService;
        }
        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            return Ok(_service.GetAll());
        }
        //[HttpGet]
        //public IActionResult GetOrdersBySupplierId(int id)
        //{
           

        //}
        //[HttpGet]
        
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
