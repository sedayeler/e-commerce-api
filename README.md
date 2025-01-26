# E-Commerce

## Project Description
This project is an e-commerce system developed using ASP.NET Core Web API. It allows users to view products, add them to the basket, create orders, and manage products via the admin panel.

## Features
- **Onion Architecture** and **CQRS Pattern** for a modular and scalable structure.
- **PostgreSQL** for database management.
- **Entity Framework Core** for database operations. 
- **Fluent Validation** for data validation.
- **JWT Token Authentication & Authorization** for secure access control.
- **SignalR** for real-time notifications.
- **MailService** using Gmail SMTP for email notifications.
- **Serilog** for logging and monitoring.
- **Global Exception Handler** for centralized error handling.

## Installation
Follow these steps to run the project:

### 1. Clone the Repository
```bash
git clone https://github.com/sedayeler/e-commerce-api
cd e-commerce-api
```

### 2. Install Dependencies
```bash
dotnet restore
```

### 3. Set Up the Database
- Edit the appsettings.json file to update the PostgreSQL connection string.
- Run migrations to create the database:
```bash
dotnet ef database update
```

### 4. Run the Application
```bash
dotnet run
```

### 5. Explore the API with Swagger
After starting the application, open your browser and go to http://localhost:7031/swagger.
