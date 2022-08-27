using WRP3.DataAccess.EFDBContext;
using WRP3.Domain.Entities;
using WRP3.IServices;
using WRP3.Services.Comman;

namespace WRP3.Services
{
    public class TestTypeRepository : Service<TestType>, ITestType
    {
        private readonly ApplicationDbContext _context;
        public TestTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<int> Save(TestType testType)
        {
            throw new NotImplementedException();
        }
    }
}
