using AutoMapper;
using EmployeeManagementApi.Data;
using EmployeeManagementApi.DTOs;
using EmployeeManagementApi.Orchestrators.Employee.Contract;
using EmployeeManagementApi.Repositories.Employee.Contract;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Orchestrators.Employee
{
    public class EmployeeOrch : IEmployeeOrch
    {
        private readonly IUnitOfWork<EmployeeManagementDbContext> _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeOrch(
            IUnitOfWork<EmployeeManagementDbContext> unitOfWork,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObj<IEnumerable<EmployeeDataDto>>> GetAllAsync(EmployeeSearchDto searchDto)
        {
            var query = _employeeRepository.GetAll().Where(e => !e.IsDeleted);
            if (!string.IsNullOrEmpty(searchDto.SearchTerm))
            {
                var term = searchDto.SearchTerm.ToLower();
                query = query.Where(e => e.FirstName.ToLower().Contains(term) ||
                                         e.LastName.ToLower().Contains(term) ||
                                         e.Email.ToLower().Contains(term) ||
                                         e.Position.ToLower().Contains(term));
            }

            var totalCount = await query.CountAsync();
            var data = await query
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .ToListAsync();
            var mapData = _mapper.Map<IEnumerable<EmployeeDataDto>>(data);

            return new ResponseObj<IEnumerable<EmployeeDataDto>>(mapData, "Success", totalCount);
        }

        public async Task<ResponseObj<EmployeeDataDto>> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetAll().Where(e => e.Id == id && !e.IsDeleted).FirstOrDefaultAsync();
            if (employee == null)
                return new ResponseObj<EmployeeDataDto>(null!, "Employee not found", success: false);
            var employeeDto = _mapper.Map<EmployeeDataDto>(employee);
            return new ResponseObj<EmployeeDataDto>(employeeDto, "Success");
        }

        public async Task<ResponseObj<EmployeeDataDto>> CreateAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Domain.Models.Employee>(employeeDto);
            employee.Id = 0; // Ensure Id is not set by client
            var createdEmployee = await _employeeRepository.AddAsync(employee);
            _unitOfWork.SaveChanges();
            var createdEmployeeDto = _mapper.Map<EmployeeDataDto>(createdEmployee);
            return new ResponseObj<EmployeeDataDto>(createdEmployeeDto, "Employee created successfully");
        }

        public async Task<ResponseObj<EmployeeDataDto>> UpdateAsync(int id, EmployeeDto employeeDto)
        {
            var employee = await _employeeRepository.GetAll().Where(e => e.Id == id && !e.IsDeleted).FirstOrDefaultAsync();
            if (employee == null)
                return new ResponseObj<EmployeeDataDto>(null!, "Employee not found", success: false);
            _mapper.Map(employeeDto, employee);
            employee.Id = id; // Ensure Id from URL is used
            employee.UpdatedAt = DateTime.UtcNow;
            var updatedEmployee = await _employeeRepository.UpdateAsync(employee);
            _unitOfWork.SaveChanges();
            var updatedEmployeeDto = _mapper.Map<EmployeeDataDto>(updatedEmployee);
            return new ResponseObj<EmployeeDataDto>(updatedEmployeeDto, "Employee updated successfully");
        }

        public async Task<ResponseObj<bool>> DeleteAsync(int id)
        {
            var employee = await _employeeRepository.GetAll().Where(e => e.Id == id && !e.IsDeleted).FirstOrDefaultAsync();
            if (employee == null)
                return new ResponseObj<bool>(false, "Employee not found", success: false);
            employee.IsDeleted = true;
            employee.UpdatedAt = DateTime.UtcNow;
            await _employeeRepository.UpdateAsync(employee);
            _unitOfWork.SaveChanges();
            return new ResponseObj<bool>(true, "Employee deleted successfully");
        }

        public async Task<ResponseObj<string>> DeleteAllAsync(int[] ids, int userId)
        {
            foreach (var id in ids)
            {
                var employee = await _employeeRepository.GetAll().Where(e => e.Id == id && !e.IsDeleted).FirstOrDefaultAsync();
                if (employee == null)
                    throw new Exception($"Employee with ID {id} not found");
                employee.IsDeleted = true;
                employee.UpdatedById = userId;
                employee.UpdatedAt = DateTime.UtcNow;
                await _employeeRepository.UpdateAsync(employee);
            }
            _unitOfWork.SaveChanges();
            return new ResponseObj<string>("Employees deleted successfully");
        }
    }
}