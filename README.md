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
