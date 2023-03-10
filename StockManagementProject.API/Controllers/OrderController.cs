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
        private readonly IGenericService<Product> _productService;
        private readonly IGenericService<OrderDetails> _orderDetailService;
        private readonly IGenericService<User> _userService;

        public OrderController(IGenericService<User> userService, IGenericService<OrderDetails> orderDetailService, IGenericService<Order> orderService, IGenericService<Product> productService)
        {


            this._userService = userService;
            this._orderDetailService = orderDetailService;
            this._orderService = orderService;

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
        [HttpGet]
        public IActionResult GetPendingOrders()
        {
            return Ok(_orderService.GetAll().Where(x => x.Status == Status.Pending).ToList());
        }
        [HttpGet]
        public IActionResult GetRejectedOrders()
        {
            return Ok(_orderService.GetAll().Where(x => x.Status == Status.Rejected).ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Order order = _orderService.GetById(id);

            return Ok(_orderService.GetById(id));
        }
        [HttpPost]
        public IActionResult AddOrder(int userId, [FromQuery] int[] productIds, [FromQuery] int[] quantities)
        {
            Order order = new Order();

            order.CreateDate = DateTime.Now;
            order.Status = Status.Pending;
            order.UserId = userId;
            _orderService.Create(order);

            for (int i = 0; i < productIds.Length; i++)
            {
                OrderDetails orderDetail = new OrderDetails();
                orderDetail.OrderId = order.Id;
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
        public IActionResult UpdateOrder(int userId, [FromQuery] int[] productIds, [FromQuery] int[] quantities)
        {
            Order order = new Order();

            order.CreateDate = DateTime.Now;
            order.Status = Status.Pending;
            order.UserId = userId;
            _orderService.Update(order);

            for (int i = 0; i < productIds.Length; i++)
            {
                OrderDetails orderDetail = new OrderDetails();
                orderDetail.OrderId = order.Id;
                orderDetail.Status = Status.Active;
                orderDetail.ProductId = productIds[i];
                orderDetail.Quantity = (short)quantities[i];
                orderDetail.UnitPrice = _productService.GetById(productIds[i]).UnitPrice;
                orderDetail.CreateDate = DateTime.Now;
                _orderDetailService.Update(orderDetail);

            }

            return CreatedAtAction("GetById", new { id = order.Id }, order);
        }
        [HttpPut]
        public IActionResult ActivateOrder(int id)
        {
            var order = _orderService.GetById(id);
            order.Status = Status.Active;
            List<OrderDetails> od = _orderDetailService.GetDefault(x=>x.OrderId == order.Id);

            for (int i = 0; i < od.Count; i++)
            {
                Product product = _productService.GetById(od[i].ProductId);
                product.UnitsInStock -= od[i].Quantity;
                _productService.Update(product);
            }
            
            return Ok(_orderService.Activate(id));
        }
        [HttpPut]
        public IActionResult CompleteOrder(int id)
        {
            var order = _orderService.GetById(id);
            order.Status = Status.Passive;
            return Ok(_orderService.Update(order));
        }
        [HttpPut]
        public IActionResult RejectOrder(int id)
        {
            var order = _orderService.GetById(id);
            order.Status = Status.Rejected;
            return Ok(_orderService.Update(order));
        }
        [HttpDelete]
        public IActionResult DeActivateOrder(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound("Sipariş Bulunamadı");
            }
            else
            {
                order.Status = Status.Passive;
                return Ok(_orderService.Update(order));
            }

        }

    }
}
