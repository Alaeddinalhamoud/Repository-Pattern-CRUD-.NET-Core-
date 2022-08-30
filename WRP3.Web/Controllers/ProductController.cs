using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WRP3.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IAPIService<Product> _apiService;
        const string API_URL = "/api/product";

        [TempData]
        public string StatusMessage { get; set; }
        public ProductController(ILogger<ProductController> logger,
            IAPIService<Product> apiService)
        {
            _logger = logger;
            _apiService = apiService;

        }
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            try
            {
                ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["CurrentFilter"] = searchString;

                var products = await _apiService.GetAll(API_URL);

                if (!string.IsNullOrEmpty(searchString))
                    products = products.Where(s => s.Name.Contains(searchString)).ToList();

                switch (sortOrder)
                {
                    case "name_desc":
                        products = products.OrderByDescending(s => s.Name).ToList();
                        break;
                    case "Date":
                        products = products.OrderBy(s => s.Created).ToList();
                        break;
                    case "date_desc":
                        products = products.OrderByDescending(s => s.Created).ToList();
                        break;
                    default:
                        products = products.OrderBy(s => s.Name).ToList();
                        break;
                }

                return View(products);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error whilte trying to get all {typeof(ProductController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }
    }
}
