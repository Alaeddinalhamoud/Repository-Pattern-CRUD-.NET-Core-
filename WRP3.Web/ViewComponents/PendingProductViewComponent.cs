using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;

namespace WRP3.Web.ViewComponents
{
    [ViewComponent]
    public class PendingProductViewComponent : ViewComponent
    {
        private readonly ILogger<PendingProductViewComponent> _logger;
        private readonly IAPIService<Product> _productService;
        private readonly IAPIService<ProductTest> _productTestService;
        private readonly IAPIService<TestType> _testTypeService;

        const string PRODUCT_API_URL = "/api/product";
        const string PRODUCT_TEST_API_URL = "/api/ProductTest";
        const string TEST_TYPE_API_URL = "/api/TestType";

        public PendingProductViewComponent(ILogger<PendingProductViewComponent> logger,
                                           IAPIService<Product> productService,
                                           IAPIService<ProductTest> productTestService,
                                           IAPIService<TestType> testTypeService)
        {
            _logger = logger;
            _productService = productService;
            _productTestService = productTestService;
            _testTypeService = testTypeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                string currentUser = User?.Identity?.Name;
                int? numberOfPendingProduct = 0;
                int? numberOfTestType = 0;

                var products = await _productService.GetAll(PRODUCT_API_URL);

                if (products is null || products.Count() <= 0) return View(model: numberOfPendingProduct);

                var productTests = await _productTestService.GetAll(PRODUCT_TEST_API_URL);

                if (productTests is null || productTests.Count() <= 0) return View(model: products?.Count());

                var currentUserProductTests = productTests.Where(x => x.CreatedBy == currentUser).ToList();

                if (currentUserProductTests is null || currentUserProductTests.Count() <= 0) return View(model: products?.Count());

                var testTypes = await _testTypeService.GetAll(TEST_TYPE_API_URL);

                if (testTypes != null || testTypes?.Count() > 0) numberOfTestType = testTypes?.Count();

                foreach (var product in products)
                {
                    var currentProductInTest = currentUserProductTests.Where(x => x.ProductId == product.Id);

                    if (currentProductInTest.Count() != numberOfTestType)
                    {
                        numberOfPendingProduct++;
                    }
                }


                return View(numberOfPendingProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing {nameof(PendingProductViewComponent)}");
                return View(0);
            }
        }

    }
}
