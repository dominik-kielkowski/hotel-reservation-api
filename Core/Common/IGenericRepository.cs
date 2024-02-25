using Microsoft.EntityFrameworkCore;

namespace Core.Common
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        DbSet<T> AccessContext();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
