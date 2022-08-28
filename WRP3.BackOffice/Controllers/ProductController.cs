using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;

namespace WRP3.BackOffice.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductController> _logger;
        private readonly IAPIService<Product> _productAPIService;

        [TempData]
        public string StatusMessage { get; set; }
        public ProductController(IHttpClientFactory httpClientFactory,
            ILogger<ProductController> logger,
            IAPIService<Product> productAPIService)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _productAPIService = productAPIService;

        }

        public async Task<IActionResult> Index()

        {
            var a = await _productAPIService.GetAll("/api/product");
            return View(a);
            //List<Product> products;
            //try
            //{


            //    var httpClient = _httpClientFactory.CreateClient("API");

            //    var httpResponseMessage = await httpClient.GetAsync("/api/product");

            //    if (httpResponseMessage.IsSuccessStatusCode)
            //    {
            //        using var contentStream =
            //            await httpResponseMessage.Content.ReadAsStreamAsync();

            //        products = await JsonSerializer.DeserializeAsync
            //            <List<Product>>(contentStream, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            //        StatusMessage = "Message";
            //        return View(products);
            //    }
            //    return View(products = null);

            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical(ex, $"Error will processing {nameof(ProductController)}");
            //    return View(products = null);
            //}
        }
    }
}
