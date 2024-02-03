namespace Core.Common
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetEntityListWithSpecAsync(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
