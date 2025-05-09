using Microsoft.AspNetCore.Mvc;
using EmployeeManagementApi.DTOs;
using EmployeeManagementApi.Orchestrators.Employee.Contract;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeOrch _employeeOrch;

        public EmployeesController(IEmployeeOrch employeeOrch)
        {
            _employeeOrch = employeeOrch;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseObj<IEnumerable<EmployeeDataDto>>>> GetAll([FromQuery] EmployeeSearchDto searchDto)
        {
            var response = await _employeeOrch.GetAllAsync(searchDto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObj<EmployeeDataDto>>> GetById(int id)
        {
            var response = await _employeeOrch.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObj<EmployeeDataDto>>> Create([FromBody] EmployeeDto employeeDto)
        {
            var response = await _employeeOrch.CreateAsync(employeeDto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObj<EmployeeDataDto>>> Update(int id, [FromBody] EmployeeDto employeeDto)
        {
            var response = await _employeeOrch.UpdateAsync(id, employeeDto);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObj<bool>>> Delete(int id)
        {
            var response = await _employeeOrch.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost("delete-all")]
        public async Task<ActionResult<ResponseObj<string>>> DeleteAll([FromBody] int[] ids)
        {
            // Assuming userId is retrieved from authentication (hardcoded for simplicity)
            int userId = 1; // Replace with actual user ID from JWT or auth
            var response = await _employeeOrch.DeleteAllAsync(ids, userId);
            return Ok(response);
        }
    }
}