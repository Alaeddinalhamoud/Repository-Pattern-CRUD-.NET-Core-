namespace WRP3.Infrastructure.APIServices.IServices
{
    public interface IAPIService<T> where T : class
    {
        Task<T?> Get(string? id, string? url);
        Task<List<T>?> GetAll(string? url);
        Task<T?> Delete(int? id, string? url);
        Task<T?> Post(T? t, string? url);
        Task<T?> Update(T? t, string? url);
    }
}
