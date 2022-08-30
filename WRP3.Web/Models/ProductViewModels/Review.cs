using System.Collections.Generic;
using WRP3.Domain.Entities;

namespace WRP3.Web.Models.ProductViewModels
{
    public class Review
    {
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public List<TestType>? TestTypes { get; set; }
        public List<ProductTest>? ProductTests { get; set; }
    }
}
