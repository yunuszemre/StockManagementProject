using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagementProject.Entities.Entities.Concreate;
using StockManagementProject.Service.Abstract;
using StockManagementProject.Entities.Enums;

namespace StockManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGenericService<Order> _orderService;
        private readonly IGenericService<Category> _categoryService;
        private readonly IGenericService<Product> _productService;
        private readonly IGenericService<Supplier> _supplierService;
        private readonly IGenericService<OrderDetails> _orderDetailService;
        private readonly IGenericService<User> _userService;

        public OrderController(IGenericService<User> userService, IGenericService<OrderDetails> orderDetailService, IGenericService<Supplier> supplierService, IGenericService<Order> orderService, IGenericService<Category> categoryService, IGenericService<Product> productService)
        {

            this._supplierService = supplierService;
            this._userService = userService;
            this._orderDetailService = orderDetailService;
            this._orderService = orderService;
            this._categoryService = categoryService;
            this._productService = productService;
        }
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAll().ToList());
        }
        [HttpGet]
        public IActionResult GetActiveOrders()
        {
            return Ok(_orderService.GetActive());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {


            return Ok(_orderService.GetById(id));
        }
        [HttpPost]
        public IActionResult AddOrder(int userId, int[] productIds, int[] quantities)
        {
            Order order = new Order();

            order.CreateDate = DateTime.Now;
            order.Status = Status.Pending;
            order.UserId = userId;
            _orderService.Create(order);

            for (int i = 0; i < productIds.Length; i++)
            {
                OrderDetails orderDetail = new OrderDetails();
                orderDetail.Status = Status.Active;
                orderDetail.ProductId = productIds[i];
                orderDetail.Quantity = (short)quantities[i];
                orderDetail.UnitPrice = _productService.GetById(productIds[i]).UnitPrice;
                orderDetail.CreateDate = DateTime.Now;
                _orderDetailService.Create(orderDetail);

            }

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
