﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WRP3.Domain.Entities;

namespace WRP3.BackOffice.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductController> _logger;

        [TempData]
        public string StatusMessage { get; set; }
        public ProductController(IHttpClientFactory httpClientFactory, ILogger<ProductController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;

        }

        public async Task<IActionResult> Index()

        {
            List<Product> products = new();
            try
            {
                var httpRequestMessage = new HttpRequestMessage
                           (HttpMethod.Get, "/api/product");

                var httpClient = _httpClientFactory.CreateClient("API");

                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    products = await JsonSerializer.DeserializeAsync
                        <List<Product>>(contentStream);
                    StatusMessage = "Message";
                    return View(products);
                }
                return View(products = null);

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error will processing {nameof(ProductController)}");
                return View(products = null);
            }
        }
    }
}
