using WRP3.DataAccess.EFDBContext;
using WRP3.IServices;
using WRP3.IServices.Common;

namespace WRP3.Services.Comman
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Product = new ProductRepository(_context);
            TestType = new TestTypeRepository(_context);
            ProductTest = new ProductTestRepository(_context);
        }

        public IProduct Product { get; private set; }

        public ITestType TestType { get; private set; }

        public IProductTest ProductTest { get; private set; }

        public void Dispose() => _context.Dispose();
    }
}
