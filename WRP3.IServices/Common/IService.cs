namespace WRP3.IServices.Common
{
    public interface IService<T> where T : class
    {
        Task<T> Get(int id);
        IQueryable<T> GetAll();
        Task<bool> Delete(int id);
        Task<bool> Add(T entity);
        Task<int> Update(int id, T request);
    }
}
