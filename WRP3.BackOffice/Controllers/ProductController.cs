using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;

namespace WRP3.BackOffice.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IAPIService<Product> _productAPIService;
        const string Product_URL = "/api/product";

        [TempData]
        public string StatusMessage { get; set; }
        public ProductController(ILogger<ProductController> logger,
            IAPIService<Product> productAPIService)
        {
            _logger = logger;
            _productAPIService = productAPIService;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _productAPIService.GetAll("/api/product"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error whilte trying to get all {typeof(ProductController)}");
                StatusMessage = $"Error: While trying to get all products {nameof(ProductController)}";
                return View(new List<Product>());
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Product { Id = 0 });
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Product product)
        {
            try
            {
                if (product is null) return View();

                product.Created = DateTime.Now;
                product.CreatedBy = "Alaeddin local";

                var entity = await _productAPIService.Post(product, Product_URL);

                StatusMessage = $"Product {product?.Name} has been added successfully";

                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to add {typeof(Product)}");
                StatusMessage = $"Error: While trying to get all products {nameof(ProductController)}";
                return View(product);
            }
        }

    }
}
