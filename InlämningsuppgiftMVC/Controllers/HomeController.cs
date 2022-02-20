using InlämningsuppgiftMVC.Models;
using InlämningsuppgiftMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InlämningsuppgiftMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var viewModel = new HomeIndexViewModel();
            viewModel.Products = new List<ProductModel>();
            viewModel.ProductForm = new ProductModel();

            using (var client = new HttpClient())
            {
                viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7113/api/products");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeIndexViewModel model)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsJsonAsync("https://localhost:7113/api/products", model.ProductForm);
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}