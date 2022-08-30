using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;

namespace WRP3.BackOffice.Controllers
{
    [Authorize, AuthorizeForScopes(ScopeKeySection = "APIScopes:UserAccess")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IAPIService<Product> _productAPIService;
        const string API_URL = "/api/product";
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;

        [TempData]
        public string StatusMessage { get; set; }
        public ProductController(ILogger<ProductController> logger,
            IAPIService<Product> productAPIService,
            ITokenAcquisition tokenAcquisition,
            IConfiguration configuration)
        {
            _logger = logger;
            _productAPIService = productAPIService;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _productAPIService.GetAll(API_URL));
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error whilte trying to get all {typeof(ProductController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
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
                    return RedirectToAction("Error", "Home");
                }

                product.Created = DateTime.Now;
                product.CreatedBy = "Alaeddin local";

                var entity = await _productAPIService.Post(product, API_URL);

                StatusMessage = $"Product {product?.Name} has been added successfully";

                return RedirectToAction("Details", new { Id = entity.Id });
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to get all products {nameof(ProductController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == 0 || id is null)
                {
                    StatusMessage = $"Error, Please check the missed data";
                    return RedirectToAction("Error", "Home");
                }

                var product = await _productAPIService.Get(id, $"{API_URL}/GetById");

                if (product is null)
                {
                    StatusMessage = $"Error, Data not found";
                    return RedirectToAction("Error", "Home");
                }

                return View(product);

            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to get all products {nameof(ProductController)}";
                _logger.LogError(ex, StatusMessage);
                return RedirectToAction("Error", "Home");
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
                    return RedirectToAction("Error", "Home");
                }

                product.LastModified = DateTime.Now;
                product.LastModifiedBy = "Alaeddin local";

                var entity = await _productAPIService.Update(product, API_URL);

                StatusMessage = $"Product Name {product?.Name} has been updated successfully";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to Edit product {nameof(ProductController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == 0 || id is null)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return RedirectToAction("Error", "Home");
                }
                var product = await _productAPIService.Get(id, $"{API_URL}/GetById");

                if (product is null)
                {
                    StatusMessage = $"Error, Data Not Found";
                    return RedirectToAction("Error", "Home");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to Edit product {nameof(ProductController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int? productId)
        {
            try
            {
                if (productId == 0 || productId is null)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return RedirectToAction("Error", "Home");
                }

                var product = await _productAPIService.Delete(productId, API_URL);

                StatusMessage = $"Product Name {product?.Name} has been deleted.";

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to Edit product {nameof(ProductController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }
    }
}
