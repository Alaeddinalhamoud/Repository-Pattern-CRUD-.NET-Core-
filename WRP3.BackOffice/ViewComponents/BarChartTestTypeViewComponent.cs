using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using WRP3.Domain.Entities;
using WRP3.Infrastructure.APIServices.IServices;
using WRP3.BackOffice.Models.ViewComponentModels;
using System.Linq;

namespace WRP3.BackOffice.ViewComponents
{
    [ViewComponent]
    public class BarChartTestTypeViewComponent : ViewComponent
    {
        private readonly ILogger<BarChartTestTypeViewComponent> _logger;
        private readonly IAPIService<ProductTest> _productTestService;
        private readonly IAPIService<TestType> _testTypeService;

        const string PRODUCT_TEST_API_URL = "/api/ProductTest";
        const string TEST_Type_API_URL = "/api/testtype";

        public BarChartTestTypeViewComponent(ILogger<BarChartTestTypeViewComponent> logger,
                                             IAPIService<ProductTest> ProductTestService,
                                             IAPIService<TestType> testTypeService)
        {
            _logger = logger;
            _productTestService = ProductTestService;
            _testTypeService = testTypeService;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            BarChartTestType barChartTestType = new BarChartTestType();
            try
            {

                var productTests = await _productTestService.GetAll(PRODUCT_TEST_API_URL);

                var testTypes = await _testTypeService.GetAll(TEST_Type_API_URL);

                int counter = 1;
                barChartTestType.TestNames = string.Empty;
                barChartTestType.NumberOfTest = string.Empty;

                foreach (var testType in testTypes)
                {
                    barChartTestType.TestNames += $"\"{testType.Name}\"";

                    barChartTestType.NumberOfTest += $"{productTests.Where(x => x.TestTypeId == testType.Id).Count()}";


                    if (counter != testTypes.Count())
                    {
                        barChartTestType.TestNames += ", ";
                        barChartTestType.NumberOfTest += ", ";
                    }

                    counter++;
                }

                return View(barChartTestType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing {nameof(BarChartTestTypeViewComponent)}");

                return View(barChartTestType);
            }
        }
    }
}
