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
    public class PendingProductTestCaseViewComponent : ViewComponent
    {
        private readonly ILogger<PendingProductTestCaseViewComponent> _logger;
        private readonly IAPIService<ProductTest> _productTestService;

        const string PRODUCT_TEST_API_URL = "/api/ProductTest";

        public PendingProductTestCaseViewComponent(ILogger<PendingProductTestCaseViewComponent> logger,
                                           IAPIService<ProductTest> productTestService)
        {
            _logger = logger;
            _productTestService = productTestService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? productId, int? testTypeId)
        {
            try
            {
                string currentUser = User?.Identity?.Name;

                var productTests = await _productTestService.GetAll(PRODUCT_TEST_API_URL);

                var currentUserProductTests = productTests.Where(x => x.CreatedBy == currentUser
                                                                   && x.ProductId == productId
                                                                   && x.TestTypeId == testTypeId).ToList();

                if (currentUserProductTests?.Any() ?? false)
                {
                    return View(new PendingProductTestCase()
                    {
                        Mark = currentUserProductTests?.FirstOrDefault()?.Mark,
                        Status = true
                    });
                }

                return View(new PendingProductTestCase()
                {
                    Mark = 0,
                    Status = false
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing {nameof(PendingProductViewComponent)}");

                return View(new PendingProductTestCase()
                {
                    Mark = 0,
                    Status = false
                });
            }
        }
    }
}
