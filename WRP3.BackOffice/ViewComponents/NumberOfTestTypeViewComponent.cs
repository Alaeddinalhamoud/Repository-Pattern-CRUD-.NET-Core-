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
    public class NumberOfTestTypeViewComponent : ViewComponent
    {
        private readonly ILogger<NumberOfTestTypeViewComponent> _logger;
        private readonly IAPIService<TestType> _testTypeService;

        const string TEST_TYPE_API_URL = "/api/TestType";

        public NumberOfTestTypeViewComponent(ILogger<NumberOfTestTypeViewComponent> logger,
                                             IAPIService<TestType> testTypeService)
        {
            _logger = logger;
            _testTypeService = testTypeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {

                var testTypes = await _testTypeService.GetAll(TEST_TYPE_API_URL);

                if (!testTypes.Any()) return View(model: 0);


                return View(model: testTypes?.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing {nameof(NumberOfTestTypeViewComponent)}");

                return View(model: 0);
            }
        }
    }
}
