using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagementProject.Entities.Entities.Concreate;
using StockManagementProject.Service.Abstract;

namespace StockManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> _service;
        private readonly IGenericService<Category> _catService;
        private readonly IGenericService<Supplier> _supplierService;

        public ProductController(IGenericService<Product> service, IGenericService<Category> catService, IGenericService<Supplier> supplierService)
        {
            this._service = service;
            this._catService = catService;
            this._supplierService = supplierService;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        public IActionResult GetActiveProducts()
        {
            return Ok(_service.GetActive());
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            product.Category = _catService.GetById(product.CategoryId);
            product.Supplier = _supplierService.GetById(product.SupplierId);
            return Ok(_service.Create(product));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            return Ok(_service.Update(product));
        }
        [HttpPut]
        public IActionResult ActivateProduct(int id)
        {
            return Ok(_service.Activate(id));
        }
        [HttpDelete]
        public IActionResult DeActivateProduct(int id)
        {
            var sup = _service.GetById(id);
            sup.Status = Entities.Enums.Status.Passive;
            return Ok(_service.Update(sup));
        }
        [HttpGet]
        public IActionResult GetBySupplierId(int id)
        {
            return Ok(_service.GetDefault(x => x.SupplierId == id).ToList());
        }
    }
}
