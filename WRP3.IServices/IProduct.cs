using WRP3.Domain.Entities;
using WRP3.IServices.Common;

namespace WRP3.IServices
{
    public interface IProduct : IService<Product>
    {
        Task<int> Save(Product product);
    }
}
