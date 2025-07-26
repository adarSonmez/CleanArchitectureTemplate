# 🚀 CleanArchitectureTemplate

CleanArchitectureTemplate is a **modern, robust starting point** for building scalable and maintainable .NET applications. Now upgraded to **.NET 9 and C# 12**, it follows the principles of **Clean Architecture** to promote separation of concerns, testability, and ease of evolution over time.

---

## ✨ Key Features

- 🏗️ **Clean Architecture Implementation**: Organized into distinct layers—Presentation, Application, Domain, Infrastructure—for clear separation of concerns.
- ⚡ **Upgraded to .NET 9 and C# 12**: Leverages the latest framework improvements and performance enhancements.
- 🐳 **Docker Support**: Easily containerize and orchestrate your application using Docker Compose.
- 🔴 **Redis Integration with Sentinel**: Implements robust caching and distributed data management with Redis, including master-slave replication and sentinel support.
- 🤖 **Semantic Kernel for AI Actions**: Integrated Semantic Kernel capabilities to empower AI-driven features within your application.
- 🛢️ **Entity Framework Core with PostgreSQL**: Simplifies database operations using EF Core with PostgreSQL provider.
- 🔄 **CQRS with MediatR**: Implements the Command Query Responsibility Segregation pattern using MediatR.
- ✅ **Fluent Validation**: Ensures robust validation for input models with automatic integration.
- 🛠️ **Custom Mapping Extensions**: Clean, handcrafted mappings for full control—no AutoMapper or third-party mapping libraries used.
- ☁️ **Azure Blob Storage Integration**: Supports file storage with Azure Storage Blobs and local storage options.
- 🔑 **JWT Authentication**: Provides secure authentication using JWT bearer tokens.
- 🔐 **ASP.NET Core Identity Mechanism**: Offers a comprehensive identity management system for user authentication and role-based authorization.
- 🌍 **Login with Facebook or Google**: Provides seamless integration with external login providers to enhance user experience.
- 📌 **Logging with Serilog**: Structured logging for easier debugging and tracking.
- 🔍 **Visualization with Seq**: Centralized logging visualization for enhanced observability.
- 📜 **Swagger UI**: Includes interactive API documentation with Swashbuckle.AspNetCore.
- ⚙️ **Global Exception Handler**: Ensures centralized and consistent error handling across the application.
- 📧 **Email Integration**: Built-in email service using MailKit for transactional emails.
- 🚀 **Response Caching**: Integrated response caching for improved performance.

---

## 🛠️ Getting Started

### Prerequisites

Before setting up the project, ensure you have the following installed:

- ⚙️ [.NET 9 SDK](https://dotnet.microsoft.com/download)
- 🖥️ [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- 🐘 [PostgreSQL](https://www.postgresql.org/download/)
- 🐳 [Docker & Docker Compose](https://docs.docker.com/get-docker/) (for containerized deployment)
- 🔴 [Redis](https://redis.io/) (integrated via Docker containers with Sentinel support)
- ☁️ Azure account (optional, for Blob Storage integration)
- 🔎 [Seq](https://datalust.co/seq) (optional, for log visualization)

---

### 📥 Installation

Follow these steps to get the solution up and running:

1. **Clone the Repository** git clone https://github.com/adarSonmez/CleanArchitectureTemplate.git2. 
2. **Navigate to the Solution Directory**  cd CleanArchitectureTemplate3. 
3. **Restore NuGet Packages**  dotnet restore4. 
4. **Configure the Database Connection**  Update the `ConnectionStrings:DefaultConnection` in `appsettings.json` (located in `Presentation/CleanArchitectureTemplate.WebAPI`) with your PostgreSQL details.
5. **Apply Migrations**  dotnet ef database update --project Infrastructure/CleanArchitectureTemplate.Persistence6. 
6. **Set Up Serilog and Seq**  
   - Ensure `appsettings.json` includes the Seq configuration under `Logging`:
     ```json
     "Logging": {
         "LogLevel": {
             "Default": "Information",
             "Microsoft.AspNetCore": "Warning"
         },
         "Seq": {
             "ServerUrl": "http://localhost:5341"
         }
     }
     ```
   - Start Seq locally or on a server and ensure the application is pointed to the correct URL.

7. **Configure Cloud Storage (Optional)**  
   Update the `Storage` section in `appsettings.json` with your provider details. Example:"Storage": {
    "Azure": "<YourAzureBlobConnectionString>",
    "AWS": "<YourAWSStorageKey>",
    "Google": "<YourGoogleCloudStorageKey>"
}
8. **Set Up External Login Providers (Optional)**  
   Update the `OAuth:Google` and `OAuth:Facebook` sections in `appsettings.json` with the appropriate client ID and secret.

9. **Configure JWT Authentication**  
   Ensure the `Jwt` section in `appsettings.json` contains the necessary values for your application's secret key, issuer, audience, and token expiration times. Example:"Jwt": {
    "SecretKey": "千里之行，始于足下。- Lao Tzu",
    "Issuer": "<YourIssuer>",
    "Audience": "<YourAudience>",
    "AccessTokenExpiration": 360,
    "RefreshTokenExpiration": 60
   }

10. **Configure Redis Settings**  
    The application is integrated with Redis (with Sentinel support) for caching and distributed data management. Update the `ConnectionStrings:Redis` in `appsettings.json`:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "<YourPostgreSQLConnectionString>",
        "Redis": "<YourRedisConnectionString>"
    }
    ```

11. **Configure AI Actions with Semantic Kernel**  
    The AI module leverages Semantic Kernel with connectors like Ollama and OpenAI. Update the `AI` section in `appsettings.json` with your desired settings:
    ```json
    "AI": {
      "Ollama": {
        "Model": "deepseek-r1:latest",
        "ServerUrl": "http://localhost:11434"
      },
      "OpenAI": {
        "Model": "google/gemini-2.0-pro-exp-02-05:free",
        "ApiKey": "",
        "ServerUrl": "https://openrouter.ai/api/v1"
      }
    }
    ```

12. **Configure Email Settings (Optional)**  
    If you plan to use email functionality, configure the email settings in `appsettings.json`:
    ```json
    "Email": {
        "SmtpServer": "<YourSmtpServer>",
        "Port": 587,
        "Username": "<YourEmailUsername>",
        "Password": "<YourEmailPassword>",
        "EnableSsl": true
    }
    ```

---

### 🐳 Docker Deployment

For a containerized setup, use the provided `docker-compose.yml`. This file defines services for:

- **Web API**: Runs the ASP.NET Core application.
- **PostgreSQL**: Database service.
- **Redis (Master, Slaves, and Sentinels)**: Ensures high availability caching.
- **Ollama**: AI service container for Semantic Kernel integration.
- **Seq**: Log visualization service.

To launch the containers:

1. **Ensure Docker is running** on your machine.
2. **Build and start the containers** using Docker Compose: docker-compose up --build
3. The API will be available at:  
   - HTTP: `http://localhost:8080`
   - HTTPS: `http://localhost:8081`
4. Seq can be accessed at `http://localhost:5341` (or as configured in your environment).

---

### 🚀 Usage

1. **Run the Application (Non-Docker)**  dotnet run --project Presentation/CleanArchitectureTemplate.WebAPI

2. **Access Swagger UI**  
   Open your browser and navigate to:  
   🌐 `http://localhost:5000/swagger` (or the port defined in your environment) for interactive API documentation.

3. **Monitor Logs**  
   View application logs in Seq by navigating to:  
   🌐 `http://localhost:5341` (or your configured Seq server URL).

4. **Available Authentication Endpoints**  
   The template provides several authentication methods:
   - `/api/Auth/Login` - Internal login with username/password
   - `/api/Auth/GoogleLogin` - Google OAuth login
   - `/api/Auth/FacebookLogin` - Facebook OAuth login (note: typo in current implementation as "FacobookLogin")
   - `/api/Auth/RefreshToken` - Refresh JWT tokens
   - `/api/Auth/RevokeRefreshToken` - Revoke refresh tokens
   - `/api/Auth/Logout` - User logout

---

## 🏗️ Project Structure

The solution is organized into the following layers:

### Core Layer
- **📦 Domain Layer**  
  - `CleanArchitectureTemplate.Domain`: Defines domain entities, value objects, and interfaces.
  
- **🧠 Application Layer**  
  - `CleanArchitectureTemplate.Application`: Contains business logic, DTOs, commands, queries, validators, and interfaces for all services.

### Infrastructure Layer
- **🔌 Infrastructure Layer**  
  - `CleanArchitectureTemplate.Infrastructure`: Implements external services like file storage, email sending, JWT authentication, and Redis integration.
  
- **💾 Persistence Layer**  
  - `CleanArchitectureTemplate.Persistence`: Manages data access with Entity Framework Core, including database contexts and repositories.
  
- **🤖 AI Layer**  
  - `CleanArchitectureTemplate.AI`: Provides AI actions leveraging Semantic Kernel for tasks such as language detection, translation, and summarization.
  
- **📡 Realtime Communication Layer**  
  - `CleanArchitectureTemplate.RealtimeCommunication`: Supports real-time communication features using SignalR.

- **📨 Messaging Layer**  
  - `CleanArchitectureTemplate.Messaging`: Handles message processing and communication patterns.

### Presentation Layer
- **🎨 Presentation Layer**  
  - `CleanArchitectureTemplate.WebAPI`: The ASP.NET Core Web API project serving as the entry point with controllers, middleware, and configuration.

---

## 🔧 Key Technical Features

### Repository Pattern
The template implements a comprehensive repository pattern with:
- **Read Repository**: Optimized for query operations with filtering, pagination, and includes
- **Write Repository**: Handles CRUD operations with both hard and soft delete capabilities
- **Generic Implementation**: Type-safe operations for all entities inheriting from `BaseEntity`

### Validation System
- **Fluent Validation**: Automatic validation with custom filters
- **Model State Suppression**: Clean API responses without default model state errors
- **Validation Filter**: Custom validation filter for consistent error handling

### Caching Strategy
- **Response Caching**: Built-in HTTP response caching
- **Redis Caching**: Distributed caching with StackExchange.Redis
- **Configurable Cache Settings**: Flexible caching configuration per endpoint

### Security Features
- **JWT Bearer Authentication**: Secure token-based authentication
- **External OAuth Providers**: Google and Facebook login integration
- **Authorization Policies**: Role-based and policy-based authorization
- **Content Security Policy**: Built-in CSP middleware

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch for your feature:  git checkout -b feature/YourFeature
3. Commit your changes:  git commit -m "Add YourFeature"
4. Push to the branch:  git push origin feature/YourFeature
5. Open a pull request.

✅ **Pro Tip**: Ensure your code follows the project's coding standards and includes appropriate tests.

---

## 📝 Development Notes

### Known Issues
- There's a typo in the AuthController: `FacobookLogin` should be `FacebookLogin`
- The SignalR package version in `CleanArchitectureTemplate.RealtimeCommunication` uses an older version (1.2.0) - consider updating to the latest version

### Recommended Improvements
- Add comprehensive unit tests for repository implementations
- Implement health checks for external dependencies
- Add API versioning support
- Consider implementing the Outbox pattern for reliable messaging

---

## 📜 License

This project is licensed under the [MIT License](https://github.com/adarSonmez/CleanArchitectureTemplate/blob/master/LICENSE).

---

## 🙌 Acknowledgements

This template is inspired by the principles outlined in 📘 [*Clean Architecture: A Craftsman's Guide to Software Structure and Design* by Robert C. Martin](https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164).

---

### 🔗 Links

- 🌟 [GitHub Repository](https://github.com/adarSonmez/CleanArchitectureTemplate)  
- 🛠️ [Official .NET Documentation](https://docs.microsoft.com/dotnet/)  
- 🐳 [Docker Documentation](https://docs.docker.com/)  
- 🔴 [Redis Documentation](https://redis.io/)  
- 🤖 [Semantic Kernel Documentation](https://github.com/microsoft/semantic-kernel)
- 📧 [MailKit Documentation](https://github.com/jstedfast/MailKit)
- ✅ [FluentValidation Documentation](https://docs.fluentvalidation.net/)
