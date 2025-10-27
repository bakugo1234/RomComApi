# RomCom API
A social media API similar to Discord, Amino, and Instagram, built with ASP.NET Core 8.0 using a clean, layered architecture pattern.

[![License: Private](https://img.shields.io/badge/License-Private-red.svg)](#)



## 🚀 Description
This project, originally named SocialApp, is a social media API built using ASP.NET Core 8.0. It follows a clean, layered architecture pattern to ensure separation of concerns and maintainability. The API provides authentication endpoints and is designed to be extended with features such as user profiles, social interactions, and real-time messaging.



## 📑 Table of Contents
- [🚀 Description](#-description)
- [🏗️ Architecture](#️-architecture)
- [🛠️ Tech Stack](#️-tech-stack)
- [🔧 Installation](#-installation)
- [💻 Usage](#-usage)
- [🔑 Authentication Endpoints](#-authentication-endpoints)
- [📝 API Response Format](#-api-response-format)
- [📁 Project Structure](#-project-structure)
- [⚙️ Configuration](#️-configuration)
- [✨ Key Features](#-key-features)
- [📚 Architecture Documentation](#-architecture-documentation)
- [🔄 Migration from SocialApp](#-migration-from-socialapp)
- [📝 TODO](#-todo)
- [🤝 Contributing](#-contributing)
- [📄 License](#-license)
- [🔗 Important Links](#-important-links)
- [📝 Footer](#-footer)



## 🏗️ Architecture
This project follows a **clean, layered architecture pattern** with the following key components:

- **IServiceResult Pattern** - Non-generic interface with dynamic ResultData for consistent API responses
- **Auto-Registration** - Services registered via attributes (`[ScopedService]`, `[TransientService]`, `[SingletonService]`)
- **GlobalLogic Helper** - Standardized response building and audit logging
- **Clean Separation** - Service, Repository, Model, and Common layers for maintainability
- **Repository Pattern** - Data access abstraction with Dapper and stored procedures
- **Dependency Injection** - Built-in .NET Core DI container for loose coupling



## 🛠️ Tech Stack
- **ASP.NET Core 8.0**: Backend framework
- **C# 12**: Primary programming language with latest language features
- **SQL Server**: Database with stored procedures
- **Dapper**: Micro ORM for data access
- **JWT Bearer**: Authentication and authorization
- **Swagger/OpenAPI**: API documentation
- **Mapster**: Object mapping
- **Newtonsoft.Json**: JSON serialization
- **Microsoft.Data.SqlClient**: SQL Server connectivity



## 🔧 Installation



### Prerequisites
- **.NET 8.0 SDK**: Required to build and run the API
- **SQL Server**: Used as the database (LocalDB, Express, or Full version)
- **Visual Studio 2022** or **VS Code**: For development (optional)
- **Git**: For cloning the repository



### Steps
1.  **Clone the repository:**
    ```bash
    git clone https://github.com/bakugo1234/RomComApi.git
    cd RomComApi
    ```
2.  **Navigate to the API directory:**
    ```bash
    cd RomCom.Api
    ```
3.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```
4.  **Set up the database:**
   - Create a new SQL Server database named `RomComMaster`
   - Run the database scripts to create tables and stored procedures (see Database Setup section below)
5.  **Update connection string (if needed):**
   - Modify `appsettings.json` if using a different database server
6.  **Run the API:**
    ```bash
    dotnet run
    ```

Access the API at:

-   **API**: <https://localhost:5001>
-   **Swagger**: <https://localhost:5001/swagger>



## 💻 Usage
The RomCom API provides authentication and authorization functionalities. You can use it to create a social media platform similar to Discord, Amino, and Instagram. The API includes endpoints for user login, token refresh, password changes, and user information retrieval.

### API Testing Examples

#### 1. Login Request
```bash
POST /api/Auth/Login
Content-Type: application/json

{
  "userName": "admin",
  "password": "password123"
}
```

#### 2. Get Current User (with JWT token)
```bash
GET /api/Auth/GetCurrentUser
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

#### 3. Health Check
```bash
GET /api/Auth/health/database
```

#### 4. Change Password
```bash
POST /api/Auth/ChangePassword
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "currentPassword": "oldpassword",
  "newPassword": "newpassword123",
  "confirmPassword": "newpassword123"
}
```

### Real-World Use Case
Imagine building a community platform where users can connect, share content, and interact with each other. The RomCom API provides the necessary authentication and basic user management features to get started. You can extend it with features like user profiles, social features (friends, posts), file uploads, and real-time messaging to create a fully functional social media application.



## 🔑 API Endpoints

### Authentication Endpoints
| Endpoint                | Method | Auth Required | Description             |
| :---------------------- | :----- | :------------ | :---------------------- |
| `/api/Auth/Login`       | POST   | No            | User login              |
| `/api/Auth/RefreshToken` | POST   | No            | Refresh JWT token       |
| `/api/Auth/ChangePassword`| POST   | Yes           | Change password         |
| `/api/Auth/Logout`      | POST   | Yes           | User logout             |
| `/api/Auth/GetCurrentUser`| GET    | Yes           | Get current user info   |

### Health Check Endpoints
| Endpoint                | Method | Auth Required | Description             |
| :---------------------- | :----- | :------------ | :---------------------- |
| `/api/Auth/health/database` | GET   | No            | Database connection health check |



## 📝 API Response Format
All endpoints return the standard `IServiceResult` format:



### Success Response
```json
{
  "status": true,
  "message": "Success",
  "resultData": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "base64-encoded-token",
    "user": {
      "id": 1,
      "userName": "admin",
      "email": "admin@romcom.com",
      "roleName": "Administrator"
    }
  },
  "statusCode": 200,
  "metaData": null
}
```



### Error Response
```json
{
  "status": false,
  "message": "Invalid username or password",
  "resultData": null,
  "statusCode": 501,
  "metaData": null
}
```



## 📁 Project Structure
```
RomCom/
├── RomCom.Api/                    # Web API Layer
│   ├── Controllers/               # API Controllers
│   │   ├── AuthController.cs      # Authentication endpoints
│   │   └── Base/                  # Base controller utilities
│   ├── Startup/                   # Configuration and middleware
│   │   ├── ActionFilters/         # Custom action filters
│   │   └── Middlewares/           # Custom middleware
│   ├── Program.cs                 # Application entry point
│   └── appsettings.json           # Configuration
├── RomCom.Service/                # Business Logic Layer
│   ├── Services/                  # Business services
│   │   ├── LoginService.cs        # Authentication service
│   │   └── Contracts/             # Service interfaces
│   ├── Data/Model/                # Service result models
│   └── Setup/Global/              # Global utilities
├── RomCom.Repository/             # Data Access Layer
│   ├── Repositories/              # Repository implementations
│   │   ├── AuthRepository.cs      # Authentication repository
│   │   └── Contracts/             # Repository interfaces
│   └── Setup/                     # Database setup
│       ├── Provider/              # Database providers
│       └── DTOs/                  # Data transfer objects
├── RomCom.Model/                  # Data Models Layer
│   ├── ViewModels/                # API view models
│   └── DTOs/                      # Data transfer objects
└── RomCom.Common/                 # Shared Utilities
    ├── ServiceInstallers/         # Auto-registration system
    │   ├── Attributes/            # Service attributes
    │   └── Extensions/            # Registration extensions
    ├── Enums/                     # Common enumerations
    └── Model/                     # Common models
```



## ⚙️ Configuration



### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Command Timeout=0;Initial Catalog=RomComMaster;"
  },
  "AppSettings": {
    "JWTSecret": "u7Lx93Aq0bVt16dRfYpN4wZs2kHmE5gXjQhC9rT8oU",
    "TokenExpiryHours": 24,
    "Issuer": "RomCom",
    "Audience": "RomComUsers"
  }
}
```

### Database Setup
The API uses SQL Server with stored procedures. You need to create the following database objects:

#### Required Tables
- `auth.tbl_users` - User accounts
- `auth.tbl_refresh_tokens` - JWT refresh tokens

#### Required Stored Procedures
- `auth.sp_Auth_ValidateUser` - User authentication
- `auth.sp_Auth_GetUserById` - Get user by ID
- `auth.sp_Auth_GetUserByUserName` - Get user by username
- `auth.sp_Auth_CreateUser` - Create new user
- `auth.sp_Auth_UpdatePassword` - Update user password
- `auth.sp_Auth_UpdateLastLogin` - Update last login date
- `auth.sp_Auth_GetPasswordHash` - Get user password hash
- `auth.sp_Auth_CreateRefreshToken` - Create refresh token
- `auth.sp_Auth_GetRefreshToken` - Get refresh token
- `auth.sp_Auth_InvalidateRefreshToken` - Invalidate refresh token
- `auth.sp_Auth_InvalidateAllUserRefreshTokens` - Invalidate all user tokens



## ✨ Key Features
✅ **IServiceResult Pattern** - Non-generic, flexible response format
✅ **Auto-Registration** - Attribute-based service registration
✅ **JWT Authentication** - Secure token-based auth
✅ **GlobalLogic Helper** - Consistent response building
✅ **Audit Logging Support** - Built-in change tracking
✅ **Swagger Documentation** - Interactive API docs
✅ **Exception Middleware** - Global error handling
✅ **Base Controller** - Reusable auth utilities



## 📚 Architecture Documentation
The project follows standard .NET Core architectural patterns:

- **Clean Architecture** - Separation of concerns across multiple layers
- **Repository Pattern** - Data access abstraction
- **Service Layer Pattern** - Business logic encapsulation
- **Dependency Injection** - Loose coupling and testability
- **Attribute-based Registration** - Automatic service discovery



## 🔄 Migration from SocialApp
This project was renamed from **SocialApp** to **RomCom**. All namespaces, references, and configurations have been updated.



## 📝 TODO

### ✅ Completed Features
-   [x] **Authentication System** - JWT-based authentication with login/logout
-   [x] **Repository Pattern** - AuthRepository with stored procedure integration
-   [x] **Service Layer** - LoginService with business logic
-   [x] **API Controllers** - AuthController with all auth endpoints
-   [x] **Database Integration** - Dapper with SQL Server stored procedures
-   [x] **Auto-Registration** - Attribute-based service registration
-   [x] **Health Checks** - Database connection health monitoring
-   [x] **Swagger Documentation** - Interactive API documentation
-   [x] **Exception Middleware** - Global error handling
-   [x] **Base Controller** - Reusable authentication utilities

### 🔄 In Progress
-   [ ] **Refresh Token Implementation** - Complete refresh token validation logic
-   [ ] **Password Change** - Complete password change functionality
-   [ ] **User Registration** - Add user registration endpoint

### 📋 Pending Features
-   [ ] **Database Schema** - Create actual database tables and stored procedures
-   [ ] **Password Hashing** - Implement BCrypt for secure password hashing
-   [ ] **User Management** - Complete user CRUD operations
-   [ ] **User Profiles** - User profile management system
-   [ ] **Social Features** - Friends, posts, comments system
-   [ ] **File Uploads** - Image and file upload functionality
-   [ ] **Real-time Messaging** - SignalR integration for chat
-   [ ] **Notifications** - Push notification system
-   [ ] **Search Functionality** - User and content search
-   [ ] **Pagination** - Meta pagination for large datasets
-   [ ] **Email Verification** - Email verification system
-   [ ] **Password Reset** - Forgot password functionality



## 🤝 Contributing
Contributions are welcome! Please follow these steps to contribute:

1.  Fork the repository.
2.  Create a new branch for your feature or bug fix.
3.  Make your changes and commit them with descriptive messages.
4.  Submit a pull request.



## 📄 License
This is a private project. All rights reserved.



## 🔗 Important Links
-   [Repository URL](https://github.com/bakugo1234/RomComApi)



## 📝 Footer
Made with ❤️ by [bakugo1234](https://github.com/bakugo1234).

-   **Repository**: [RomComApi](https://github.com/bakugo1234/RomComApi)
-   **Author**: bakugo1234
-   **Contact**: (No contact information was provided in the context)

Feel free to fork, like, star, and raise issues!

