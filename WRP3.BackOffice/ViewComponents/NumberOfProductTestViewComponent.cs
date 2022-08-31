using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using System.Linq;

namespace WRP3.BackOffice.ViewComponents
{
    [ViewComponent]
    public class NumberOfProductTestViewComponent : ViewComponent
    {
        private readonly ILogger<NumberOfProductTestViewComponent> _logger;
        private readonly IAPIService<ProductTest> _productTestService;

        const string PRODUCT_TEST_API_URL = "/api/ProductTest";

        public NumberOfProductTestViewComponent(ILogger<NumberOfProductTestViewComponent> logger,
                                             IAPIService<ProductTest> ProductTestService)
        {
            _logger = logger;
            _productTestService = ProductTestService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {

                var productTests = await _productTestService.GetAll(PRODUCT_TEST_API_URL);

                if (!productTests.Any()) return View(model: 0);


                return View(model: productTests?.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing {nameof(NumberOfProductTestViewComponent)}");

                return View(model: 0);
            }
        }
    }
}
