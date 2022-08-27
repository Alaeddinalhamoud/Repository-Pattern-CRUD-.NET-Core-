namespace WRP3.IServices.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IProduct Product { get; }
        ITestType TestType { get; }
        IProductTest ProductTest { get; }
    }
}
