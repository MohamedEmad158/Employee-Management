using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Data
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext
    {
        EmployeeManagementDbContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        int SaveChanges();
        GenericRepository<T> GenericRepository<T>() where T : class;
    }
}