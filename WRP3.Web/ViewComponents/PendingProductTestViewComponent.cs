using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using System.Linq;
using WRP3.Web.Models.ViewComponentModels;

namespace WRP3.Web.ViewComponents
{
    [ViewComponent]
    public class PendingProductTestViewComponent : ViewComponent
    {
        private readonly ILogger<PendingProductTestViewComponent> _logger;
        private readonly IAPIService<ProductTest> _productTestService;
        private readonly IAPIService<TestType> _testTypeService;

        const string PRODUCT_TEST_API_URL = "/api/ProductTest";
        const string TEST_TYPE_API_URL = "/api/TestType";

        public PendingProductTestViewComponent(ILogger<PendingProductTestViewComponent> logger,
                                           IAPIService<ProductTest> productTestService,
                                           IAPIService<TestType> testTypeService)
        {
            _logger = logger;
            _productTestService = productTestService;
            _testTypeService = testTypeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            try
            {
                string currentUser = User?.Identity?.Name;

                var productTests = await _productTestService.GetAll(PRODUCT_TEST_API_URL);

                var currentUserProductTests = productTests.Where(x => x.CreatedBy == currentUser
                                                                   && x.ProductId == productId).ToList();

                var testTypes = await _testTypeService.GetAll(TEST_TYPE_API_URL);

                return View(new PendingProductTest()
                {
                    NumberOfTestType = testTypes?.Count(),
                    NumberOfTest = currentUserProductTests?.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing {nameof(PendingProductViewComponent)}");
                return View(0);
            }
        }
    }
}
