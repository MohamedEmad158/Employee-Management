using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.Orchestrators.Employee.Contract
{
    public interface IEmployeeOrch
    {
        Task<ResponseObj<IEnumerable<EmployeeDataDto>>> GetAllAsync(EmployeeSearchDto searchDto);
        Task<ResponseObj<EmployeeDataDto>> GetByIdAsync(int id);
        Task<ResponseObj<EmployeeDataDto>> CreateAsync(EmployeeDto employeeDto);
        Task<ResponseObj<EmployeeDataDto>> UpdateAsync(int id, EmployeeDto employeeDto);
        Task<ResponseObj<bool>> DeleteAsync(int id);
        Task<ResponseObj<string>> DeleteAllAsync(int[] ids, int userId);
    }
}