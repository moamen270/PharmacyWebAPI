namespace PharmacyWebAPI.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IBrandRepository Brand { get; }
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IOrderRepository Order { get; }
        IOrderDetailRepository OrderDetail { get; }
        IPrescriptionRepository Prescription { get; }
        IPrescriptionDetailsRepository PrescriptionDetails { get; }

        int Save();
    }
}