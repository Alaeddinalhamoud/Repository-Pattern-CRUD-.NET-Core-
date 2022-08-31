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
    public class NumberOfProductViewComponent : ViewComponent
    {
        private readonly ILogger<NumberOfProductViewComponent> _logger;
        private readonly IAPIService<Product> _productService;

        const string PRODUCT_API_URL = "/api/Product";

        public NumberOfProductViewComponent(ILogger<NumberOfProductViewComponent> logger,
                                             IAPIService<Product> productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {

                var products = await _productService.GetAll(PRODUCT_API_URL);

                if (!products.Any()) return View(model: 0);


                return View(model: products?.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing {nameof(ProductTestStatusViewComponent)}");

                return View(model: 0);
            }
        }
    }
}
