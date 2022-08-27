using WRP3.Domain.Entities;
using WRP3.IServices.Common;

namespace WRP3.IServices
{
    public interface ITestType : IService<TestType>
    {
        Task<int> Save(TestType testType);
    }
}
