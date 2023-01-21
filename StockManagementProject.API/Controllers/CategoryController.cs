using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementProject.Entities.Entities.Concreate;
using StockManagementProject.Service.Abstract;

namespace StockManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericService<Category> _service;

        public CategoryController(IGenericService<Category> service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_service.GetAll().ToList());
        }
        [HttpGet]
        public IActionResult GetActiveCategory()
        {
            return Ok(_service.GetActive());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _service.Create(category);

            return CreatedAtAction("GetById", new { id = category.Id }, category);
        }
        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            return Ok(_service.Update(category));
        }
        [HttpPut]
        public IActionResult ActivateCategory(int id)
        {
            return Ok(_service.Activate(id));
        }
        [HttpDelete]
        public IActionResult DeActivateCategory(int id)
        {
            var cat = _service.GetById(id);
            cat.Status = Entities.Enums.Status.Passive;
            return Ok(_service.Update(cat));
        }

    }
}
