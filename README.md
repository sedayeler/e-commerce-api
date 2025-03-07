# E-Commerce Web API

## Project Description
This project is an e-commerce website developed with ASP.NET Core Web API (.NET 7). Users can create accounts, log in, and securely complete their shopping transactions. Logged-in users can browse products, add them to their basket, and place orders. Through the admin panel, administrators can easily manage products and orders.

## Technologies Used
- **Backend:** Developed using **ASP.NET Core Web API**.
- **Database:** Data management is handled with **PostgreSQL**.
- **Architecture:** Implements a **layered structure** following the **Onion Architecture**.
- **Repository Management:** Data access is managed using the **Generic Repository Pattern**.
- **Query and Command Structure:** Uses **CQRS Pattern** with **MediatR** to separate queries and commands.
- **ORM:** **Entity Framework Core** is used with the **Code First** approach.
- **Validation:** **Fluent Validation** ensures input data validation.
- **Authentication & Authorization:** **JWT Token Authentication/Authorization** mechanism is implemented.
- **Authorization:** **Role-based authorization** is used to manage different user roles.
- **Real-Time Communication:** **SignalR** provides real-time notifications and communication.
- **Email Service:** Integrated with **SMTP** for email service functionality.
- **Version Control:** Code is managed using **Git & GitHub**.

![Image](https://github.com/user-attachments/assets/44708f96-5303-4ee8-bc52-05a479de7b63)
![Image](https://github.com/user-attachments/assets/06940c00-37e5-4058-9641-175a7eb2b0bd)
![Image](https://github.com/user-attachments/assets/0dbcf63b-9548-4381-a858-b3f56266345a)

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
