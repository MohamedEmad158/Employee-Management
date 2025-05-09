using EmployeeManagementApi.Data;
using EmployeeManagementApi.Domain.Models;
using System.Linq;

namespace EmployeeManagementApi.Repositories.Employee.Contract
{
    public interface IEmployeeRepository : IGenericRepository<EmployeeManagementApi.Domain.Models.Employee>
    {
        IQueryable<Domain.Models.Employee> GetAll();
    }
}