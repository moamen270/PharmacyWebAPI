namespace PharmacyWebAPI.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T>? GetAsync(int id);

        Task<T> AddAsync(T entity);

        T Update(T entity);

        T Delete(T entity);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> filter);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProperty = null);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[]? includeProperty);

        Task<IEnumerable<T>> GetAllFilterAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includeProperty);
    }
}