using EmployeeManagementApi.Data;
using EmployeeManagementApi.Domain.Models;
using EmployeeManagementApi.Repositories.Employee.Contract;
using System.Linq;

namespace EmployeeManagementApi.Repositories.Employee
{
    public class EmployeeRepository : GenericRepository<EmployeeManagementApi.Domain.Models.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeManagementDbContext context) : base(context) { }

        public IQueryable<EmployeeManagementApi.Domain.Models.Employee> GetAll()
        {
            return base.GetAll();
        }
    }
}