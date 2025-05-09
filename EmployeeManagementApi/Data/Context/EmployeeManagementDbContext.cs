using Microsoft.EntityFrameworkCore;

public class EmployeeManagementDbContext : DbContext
{
    public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options) : base(options) { }
    public DbSet<EmployeeManagementApi.Domain.Models.Employee> Employees { get; set; }
}