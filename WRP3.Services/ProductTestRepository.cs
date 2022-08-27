using WRP3.DataAccess.EFDBContext;
using WRP3.Domain.Entities;
using WRP3.IServices;
using WRP3.Services.Comman;

namespace WRP3.Services
{
    public class ProductTestRepository : Service<ProductTest>, IProductTest
    {
        private readonly ApplicationDbContext _context;
        public ProductTestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<int> Save(ProductTest productTest)
        {
            throw new NotImplementedException();
        }
    }
}
