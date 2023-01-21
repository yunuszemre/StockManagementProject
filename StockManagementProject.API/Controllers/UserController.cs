using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagementProject.Entities.Entities.Concreate;
using StockManagementProject.Service.Abstract;

namespace StockManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericService<User> _service;
        private readonly IGenericService<Product> _productService;

        public UserController(IGenericService<User> service, IGenericService<Product> productService)
        {
            this._service = service;
            this._productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_service.GetAll().ToList());
        }
        [HttpGet]
        public IActionResult GetActiveUser()
        {
            return Ok(_service.GetActive());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }
        [HttpGet]
        public IActionResult GetUsersProduct(int id)
        {
            return Ok(_productService.GetAll(x => x.SupplierId == id).ToList());
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _service.Create(user);

            return CreatedAtAction("GetById", new { id = user.Id }, user);
        }
        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            return Ok(_service.Update(user));
        }
        [HttpPut]
        public IActionResult ActivateUser(int id)
        {
            return Ok(_service.Activate(id));
        }
        [HttpDelete]
        public IActionResult DeActivateUser(int id)
        {
            var cat = _service.GetById(id);
            cat.Status = Entities.Enums.Status.Passive;
            return Ok(_service.Update(cat));
        }
        [HttpGet]
        public IActionResult Login(string email, string password)
        {
            if (!_service.Any(x => x.Email == email && x.Password == password))
                return BadRequest();
            else
            {
                User loggedUser = _service.GetByDefault(x => x.Email == email && x.Password == password);
                return Ok(loggedUser);
            }

        }
    }
}
