using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.DTOs
{
    public class EmployeeDataDto
    {
        public int Id { get; set; } // Added Id for display purposes
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
}