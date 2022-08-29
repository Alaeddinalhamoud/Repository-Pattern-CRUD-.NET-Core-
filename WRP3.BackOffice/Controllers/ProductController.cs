﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;

namespace WRP3.BackOffice.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IAPIService<Product> _productAPIService;
        const string Product_API_URL = "/api/product";

        [TempData]
        public string StatusMessage { get; set; }
        public ProductController(ILogger<ProductController> logger,
            IAPIService<Product> productAPIService)
        {
            _logger = logger;
            _productAPIService = productAPIService;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _productAPIService.GetAll(Product_API_URL));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error whilte trying to get all {typeof(ProductController)}");
                StatusMessage = $"Error: While trying to get all products {nameof(ProductController)}";
                return View(new List<Product>());
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Product { Id = 0 });
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Product product)
        {
            try
            {
                if (product is null)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return View(product);
                }

                product.Created = DateTime.Now;
                product.CreatedBy = "Alaeddin local";

                var entity = await _productAPIService.Post(product, Product_API_URL);

                StatusMessage = $"Product {product?.Name} has been added successfully";

                return RedirectToAction("Details", new { Id = product.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to add {typeof(Product)}");
                StatusMessage = $"Error: While trying to get all products {nameof(ProductController)}";
                return View(product);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == 0)
                {
                    StatusMessage = $"Error, Please check the missed data";
                    return View(new Product() { Id = 0 });
                }

                var product = await _productAPIService.Get(id, $"{Product_API_URL}/GetById");

                if (product is null)
                {
                    StatusMessage = $"Error, Data not found";
                    return View(new Product() { Id = 0 });
                }

                return View(product);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to Get {typeof(Product)}");
                StatusMessage = $"Error: While trying to get all products {nameof(ProductController)}";
                return View(new Product() { Id = 0 });
            }
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            try
            {
                if (product is null)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return View(product);
                }

                product.LastModified = DateTime.Now;
                product.LastModifiedBy = "Alaeddin local";

                var entity = await _productAPIService.Update(product, Product_API_URL);

                StatusMessage = $"Product {product?.Name} has been updated successfully";

                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to Edit {typeof(Product)}");
                StatusMessage = $"Error: While trying to Edit product {nameof(ProductController)}";
                return View(product);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == 0)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return View(new Product() { Id = 0 });
                }
                var product = await _productAPIService.Get(id, $"{Product_API_URL}/GetById");

                if (product is null)
                {
                    StatusMessage = $"Error, Data Not Found";
                    return View(new Product() { Id = 0 });
                }
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to Edit {typeof(Product)}");
                StatusMessage = $"Error: While trying to Edit product {nameof(ProductController)}";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == 0)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return View(new Product() { Id = 0 });
                }

                var product = await _productAPIService.Delete(Convert.ToInt32(id), Product_API_URL);

                StatusMessage = $"Product Id {product?.Name} has been deleted.";

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to Delete {typeof(Product)}");
                StatusMessage = $"Error: While trying to Edit product {nameof(ProductController)}";
                return View();
            }
        }

    }
}
