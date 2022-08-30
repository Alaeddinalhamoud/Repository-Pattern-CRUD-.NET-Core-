using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using Microsoft.AspNetCore.Authorization;

namespace WRP3.BackOffice.Controllers
{
    [Authorize]
    public class TestTypeController : Controller
    {
        private readonly ILogger<TestTypeController> _logger;
        private readonly IAPIService<TestType> _apiService;
        const string API_URL = "/api/testtype";

        [TempData]
        public string StatusMessage { get; set; }
        public TestTypeController(ILogger<TestTypeController> logger,
            IAPIService<TestType> apiService)
        {
            _logger = logger;
            _apiService = apiService;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _apiService.GetAll(API_URL));
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to get all Test Type {nameof(TestTypeController)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new TestType { Id = 0 });
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(TestType testType)
        {
            try
            {
                if (testType is null)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return RedirectToAction("Error", "Home");
                }

                testType.Created = DateTime.Now;
                testType.CreatedBy = "Alaeddin local";

                var entity = await _apiService.Post(testType, API_URL);

                StatusMessage = $"Test Type {testType?.Name} has been added successfully";

                return RedirectToAction("Details", new { Id = entity.Id });
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to get all TestType {nameof(TestType)}";
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

                var testType = await _apiService.Get(id, $"{API_URL}/GetById");

                if (testType is null)
                {
                    StatusMessage = $"Error, Data not found";
                    return RedirectToAction("Error", "Home");
                }

                return View(testType);

            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to get all TestType {nameof(TestType)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(TestType testType)
        {
            try
            {
                if (testType is null)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return RedirectToAction("Error", "Home");
                }

                testType.LastModified = DateTime.Now;
                testType.LastModifiedBy = "Alaeddin local";

                var entity = await _apiService.Update(testType, API_URL);

                StatusMessage = $"Product {testType?.Name} has been updated successfully";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to Edit TestType {nameof(TestType)}";
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
                var product = await _apiService.Get(id, $"{API_URL}/GetById");

                if (product is null)
                {
                    StatusMessage = $"Error, Data Not Found";
                    return RedirectToAction("Error", "Home");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to Edit TestType {nameof(TestType)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int? testTypeId)
        {
            try
            {
                if (testTypeId == 0 || testTypeId is null)
                {
                    StatusMessage = $"Error, Please check the missed fields";
                    return RedirectToAction("Error", "Home");
                }

                var testType = await _apiService.Delete(testTypeId, API_URL);

                StatusMessage = $"TestType name {testType?.Name} has been deleted.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: While trying to Edit TestType {nameof(TestType)}";
                _logger.LogError(ex, StatusMessage);

                return RedirectToAction("Error", "Home");
            }
        }
    }
}
