using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagementProject.Entities.Entities.Concreate;
using StockManagementProject.Repositories.Abstract;
using StockManagementProject.Service.Abstract;

namespace StockManagementProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IGenericService<OrderDetails> service;

        public OrderDetailController(IGenericService<OrderDetails> service)
        {
            this.service = service;
        }

        //[HttpGet]
        //public IActionResult GetDetails(int id)
        //{
        //    service.GetByDefault();
        //}

    }
}
