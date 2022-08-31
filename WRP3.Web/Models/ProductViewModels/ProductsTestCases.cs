using System.Collections.Generic;
using WRP3.Domain.Entities;

namespace WRP3.Web.Models.ProductViewModels
{
    public class ProductsTestCases
    {
        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public List<TestType> TestType { get; set; } = new();
    }
}
