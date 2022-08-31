using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using System.Linq;
using WRP3.BackOffice.Models.ViewComponentModels;

namespace WRP3.BackOffice.ViewComponents
{
    [ViewComponent]
    public class ProductTestStatusViewComponent : ViewComponent
    {
        private readonly ILogger<ProductTestStatusViewComponent> _logger;
        private readonly IAPIService<ProductTest> _productTestService;

        const string PRODUCT_TEST_API_URL = "/api/ProductTest";

        public ProductTestStatusViewComponent(ILogger<ProductTestStatusViewComponent> logger,
                                             IAPIService<ProductTest> productTestService)
        {
            _logger = logger;
            _productTestService = productTestService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? productId)
        {
            try
            {
                string currentUser = User?.Identity?.Name;

                var productTests = await _productTestService.GetAll(PRODUCT_TEST_API_URL);

                var currentUserProductTests = productTests.Where(x => x.ProductId == productId).ToList();

                if (currentUserProductTests?.Any() ?? false)
                {
                    return View(new ProductTestCases()
                    {
                        NumberOfTest = currentUserProductTests?.Count(),
                        NumberOfStarts = currentUserProductTests.Sum(x => x.Mark) / currentUserProductTests.Count()
                    });
                }

                return View(new ProductTestCases()
                {
                    NumberOfTest = 0,
                    NumberOfStarts = 0
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing {nameof(ProductTestStatusViewComponent)}");

                return View(new ProductTestCases()
                {
                    NumberOfTest = 0,
                    NumberOfStarts = 0
                });
            }
        }
    }
}
