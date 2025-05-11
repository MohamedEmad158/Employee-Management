# Employee Management Backend

## Overview
This is the backend of the Employee Management application, built with **.NET Core Web API** using Onion Architecture, Repository Pattern, UnitOfWork, and SQL Server. It provides an API to manage employee data.

## Features
- `GET /api/Employees`: Returns a list of employees with ID, First Name, Last Name, Email, and Position.
- Response format:
  ```json
  {
      "data": [
          {
              "id": 1,
              "firstName": "Mohamed",
              "lastName": "Emad",
              "email": "mohamedemadmb@gmail.com",
              "position": ".Net Developer"
          },
          ...
      ],
      "message": "Success",
      "success": true,
      "totalCount": 21
  }

Prerequisites

.NET 8 SDK
SQL Server
Entity Framework Core CLI (for migrations)

Setup

Clone the repository:git clone https://github.com/MohamedEmad158/Employee-Management.git
cd Employee-Management


Update the database connection string in appsettings.json:{
    "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=EmployeeManagement;Trusted_Connection=True;"
    }
}


Run migrations to create the database:dotnet ef migrations add InitialCreate
dotnet ef database update


Run the API:dotnet run


Access the Swagger UI at https://localhost:7047/swagger to test the API.

Notes

Enable CORS in Program.cs to allow Frontend integration:app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


The API is designed to work with the Angular Frontend at http://localhost:4200.
For additional endpoints (e.g., CRUD operations), please contact me.

Contact
For any questions, reach out to Mohamed Emad at mohamedemadmb@gmail.com.```
