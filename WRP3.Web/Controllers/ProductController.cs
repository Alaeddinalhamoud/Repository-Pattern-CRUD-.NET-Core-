using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using WRP3.Web.Models.ProductViewModels;
using System.Collections.Generic;

namespace WRP3.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IAPIService<Product> _productService;
        private readonly IAPIService<TestType> _testTypeService;
        private readonly IAPIService<ProductTest> _productTestService;

        const string PRODUCT_API_URL = "/api/product";
        const string PRODUCT_TEST_API_URL = "/api/ProductTest";
        const string TEST_TYPE_API_URL = "/api/TestType";

        [TempData]
        public string StatusMessage { get; set; }
        public ProductController(ILogger<ProductController> logger,
            IAPIService<Product> productService,
            IAPIService<TestType> testTypeService,
            IAPIService<ProductTest> productTestService)
        {
            _logger = logger;
            _productService = productService;
            _testTypeService = testTypeService;
            _productTestService = productTestService;

        }
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            try
            {
                ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["CurrentFilter"] = searchString;

                var products = await _productService.GetAll(PRODUCT_API_URL);

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
                StatusMessage = $"Error whilte trying to get all {nameof(ProductController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Review(int? Id)
        {
            try
           {
                if (Id == 0 || Id is null)
                {
                    StatusMessage = $"Error Empty data passed {nameof(ProductController)}";
                    return RedirectToAction("Error", "Home");
                }
                Product? product = await _productService.Get(Id, $"{PRODUCT_API_URL}/GetById");
                List<TestType>? testTypes = await _testTypeService.GetAll(TEST_TYPE_API_URL);
                List<ProductTest>? productTests = await _productTestService.GetAll(PRODUCT_TEST_API_URL);

                var reviewModel = new Review()
                {
                    ProductId = product?.Id,
                    ProductName = product?.Name,
                    TestTypes = testTypes,
                    ProductTests = productTests.Where(x => x.Product?.Id.Equals(product?.Id) ?? false).ToList()
                };

                return View(reviewModel);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error whilte trying to get all product review {nameof(ProductController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }

        }
    }
}
