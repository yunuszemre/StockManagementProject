using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagementProject.Entities.Entities.Concreate;
using StockManagementProject.Service.Abstract;

namespace StockManagementProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGenericService<Order> _orderService;
        private readonly IGenericService<Category> _categoryService;
        private readonly IGenericService<Supplier> _supplierService;
        private readonly IGenericService<OrderDetails> _orderDetailService;
        private readonly IGenericService<User> _userService;

        public OrderController(IGenericService<User> userService, IGenericService<OrderDetails> orderDetailService, IGenericService<Supplier> supplierService, IGenericService<Order> orderService, IGenericService<Category> categoryService)
        {
            
            this._supplierService = supplierService;
            this._userService = userService;
            this._orderDetailService = orderDetailService;
            this._orderService = orderService;
            this._categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAll().ToList());
        }
        [HttpGet]
        public IActionResult GetActiveOrder()
        {
            return Ok(_orderService.GetActive());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            

            return Ok(_orderService.GetById(id));
        }
        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            order.Supplier = _supplierService.GetById(order.SupplierId);
            order.User = _userService.GetById(order.UserId);
            
            _orderService.Create(order);
            
            return CreatedAtAction("GetById", new { id = order.Id }, order);
        }
        [HttpPut]
        public IActionResult UpdateOrder(Order order)
        {
            return Ok(_orderService.Update(order));
        }
        [HttpPut]
        public IActionResult ActivateOrder(int id)
        {
            return Ok(_orderService.Activate(id));
        }
        [HttpDelete]
        public IActionResult DeActivateOrder(int id)
        {
            var cat = _orderService.GetById(id);
            cat.Status = Entities.Enums.Status.Passive;
            return Ok(_orderService.Update(cat));
        }

    }
}
