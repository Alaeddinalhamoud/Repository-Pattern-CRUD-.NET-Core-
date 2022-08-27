using WRP3.DataAccess.EFDBContext;
using WRP3.Domain.Entities;
using WRP3.IServices;
using WRP3.Services.Comman;

namespace WRP3.Services
{
    public class ProductRepository : Service<Product>, IProduct
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
