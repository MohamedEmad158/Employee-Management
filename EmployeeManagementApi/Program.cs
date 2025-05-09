using Microsoft.EntityFrameworkCore;
using EmployeeManagementApi.Data;
using EmployeeManagementApi.Repositories.Employee.Contract;
using EmployeeManagementApi.Repositories.Employee;
using EmployeeManagementApi.Orchestrators.Employee.Contract;
using EmployeeManagementApi.Orchestrators.Employee;
using AutoMapper;
using FluentValidation.AspNetCore;
using EmployeeManagementApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<EmployeeManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add UnitOfWork and Repositories
builder.Services.AddScoped<IUnitOfWork<EmployeeManagementDbContext>, UnitOfWork<EmployeeManagementDbContext>>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Add Orchestrators
builder.Services.AddScoped<IEmployeeOrch, EmployeeOrch>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();