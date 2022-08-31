using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using WRP3.Web.Models.ProductTestModels;

namespace WRP3.Web.Controllers
{
    [Authorize]
    public class ProductTestController : Controller
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
        public ProductTestController(ILogger<ProductController> logger,
            IAPIService<Product> productService,
            IAPIService<TestType> testTypeService,
            IAPIService<ProductTest> productTestService)
        {
            _logger = logger;
            _productService = productService;
            _testTypeService = testTypeService;
            _productTestService = productTestService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TestIt(int testTypeId, int productId,
                                                string productName, string testTypeName)
        {

            var productTests = await _productTestService.GetAll(PRODUCT_TEST_API_URL);
            var CurrentProductTest = productTests?.FirstOrDefault(x => x.ProductId.Equals(productId) &&
                                                                       x.CreatedBy.Equals(User?.Identity?.Name) &&
                                                                       x.TestTypeId.Equals(testTypeId));

            if (CurrentProductTest != null)
            {
                return View(new ProductTestView()
                {
                    ProductId = productId,
                    TestTypeId = testTypeId,
                    ProductName = productName,
                    TestTypeName = testTypeName,
                    ProductTestId = CurrentProductTest.Id,
                    Mark = CurrentProductTest.Mark
                });
            }

            return View(new ProductTestView()
            {
                ProductId = productId,
                TestTypeId = testTypeId,
                ProductName = productName,
                TestTypeName = testTypeName,
                ProductTestId = 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> TestIt(ProductTest productTest)
        {
            try
            {
                if (productTest is null)
                {
                    StatusMessage = $"Error Empty data passed {nameof(ProductTestController)}";
                    return RedirectToAction("Error", "Home");
                }

                if (productTest.Id == 0)
                {
                    productTest.CreatedBy = User?.Identity?.Name;
                    productTest.Created = DateTime.Now;
                    await _productTestService.Post(productTest, PRODUCT_TEST_API_URL);
                    StatusMessage = $"Test Case has been added  successfully ⭐";
                }
                else
                {
                    ProductTest entity = new();
                    entity = await _productTestService.Get(productTest.Id, $"{PRODUCT_TEST_API_URL}/GetById");
                    entity.Mark = productTest.Mark;
                    entity.LastModified = DateTime.Now;
                    entity.LastModifiedBy = User?.Identity?.Name;
                    entity = await _productTestService.Update(entity, PRODUCT_TEST_API_URL);
                    StatusMessage = $"Test Case has been Updated  successfully 💡";
                }

                return RedirectToAction("ProductTestCases", "Product", new { id = productTest.ProductId });
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error whilte trying to submit test {nameof(ProductTestController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }
    }
}
