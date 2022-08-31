using WRP3.Domain.Comman;

namespace WRP3.Domain.Entities
{
    public class ProductTest : BaseAuditableEntity
    {
        public int? ProductId { get; set; }
        public int? TestTypeId { get; set; }
        public int Mark { get; set; }
    }
}
