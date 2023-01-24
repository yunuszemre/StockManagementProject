using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockManagementProject.Entities.Entities.Concreate;

namespace StockManagementProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        string uri = "https://localhost:7179";
        public async Task<IActionResult> Index()
        {
            List<Category> categories = new List<Category>(); 
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{uri}/api/Category/GetAllCategories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }
            return View(categories);
        }
    }
}
