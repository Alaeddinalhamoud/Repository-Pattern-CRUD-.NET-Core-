namespace WRP3.Infrastructure.APIServices.IServices
{
    public interface IAPIService<T> where T : class
    {
        Task<T> Get(string id, string url);
        Task<List<T>> GetAll(string url);
        Task<bool> Delete(int? id, string urlen);
        Task<bool> Post(T t, string url);
    }
}
