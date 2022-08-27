using WRP3.Domain.Comman;

namespace WRP3.Domain.Entities
{
    public class ProductTest : BaseAuditableEntity
    {
        public Product? Product { get; set; }
        public TestType? TestType { get; set; }
        public int Mark { get; set; }
    }
}
