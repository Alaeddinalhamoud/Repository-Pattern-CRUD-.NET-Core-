using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using WRP3.Infrastructure.GoogleRecaptcha.IServices;
using WRP3.Infrastructure.GoogleRecaptcha.Models;
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
        private readonly IGoogleRecaptchaService _googleRecaptchaService;

        const string PRODUCT_API_URL = "/api/product";
        const string PRODUCT_TEST_API_URL = "/api/ProductTest";
        const string TEST_TYPE_API_URL = "/api/TestType";

        [TempData]
        public string StatusMessage { get; set; }
        public ProductTestController(ILogger<ProductController> logger,
            IAPIService<Product> productService,
            IAPIService<TestType> testTypeService,
            IAPIService<ProductTest> productTestService,
            IGoogleRecaptchaService googleRecaptchaService)
        {
            _logger = logger;
            _productService = productService;
            _testTypeService = testTypeService;
            _productTestService = productTestService;
            _googleRecaptchaService = googleRecaptchaService;

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
        public async Task<IActionResult> TestIt(ProductTestView productTestView)
        {
            try
            {
                if (productTestView is null)
                {
                    StatusMessage = $"Error Empty data passed {nameof(ProductTestController)}";
                    return RedirectToAction("Error", "Home");
                }

                GoogleReCaptcha googleRecaptchaResponse = await _googleRecaptchaService
                            .SiteVerify(new GoogleReCaptcha()
                            {
                                GoogleRecaptchaToken = productTestView.GoogleRecaptchaToken
                            });

                if (!googleRecaptchaResponse.Success)
                {
                    StatusMessage = $"Error, Please Make sure Google Recaptch is checked";
                    return View(productTestView);
                }

                if (productTestView.ProductTestId == 0)
                {
                    ProductTest entity = new();

                    entity.CreatedBy = User?.Identity?.Name;
                    entity.Created = DateTime.Now;
                    entity.Mark = productTestView.Mark;
                    entity.ProductId = productTestView.ProductId;
                    entity.TestTypeId = productTestView.TestTypeId;

                    await _productTestService.Post(entity, PRODUCT_TEST_API_URL);
                    StatusMessage = $"Test Case has been added  successfully ⭐";
                }
                else
                {
                    ProductTest entity = new();

                    entity = await _productTestService.Get(productTestView.ProductTestId, $"{PRODUCT_TEST_API_URL}/GetById");
                    entity.Mark = productTestView.Mark;
                    entity.LastModified = DateTime.Now;
                    entity.LastModifiedBy = User?.Identity?.Name;

                    entity = await _productTestService.Update(entity, PRODUCT_TEST_API_URL);
                    StatusMessage = $"Test Case has been Updated  successfully 💡";
                }

                return RedirectToAction("ProductTestCases", "Product", new { id = productTestView.ProductId });
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
