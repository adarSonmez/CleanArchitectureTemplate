# 🚀 CleanArchitectureTemplate

CleanArchitectureTemplate is a **modern, robust starting point** for building scalable and maintainable .NET applications. It follows the principles of **Clean Architecture** to promote separation of concerns, testability, and ease of evolution over time.

---

## ✨ Key Features

- 🏗️ **Clean Architecture Implementation**: Organized into distinct layers—Presentation, Application, Domain, Infrastructure—for clear separation of concerns.
- 🟢 **.NET 8 and C# 12**: Leverages the latest .NET SDK features and performance improvements.
- 🛢️ **Entity Framework Core with PostgreSQL**: Simplifies database operations using EF Core with PostgreSQL provider.
- 🔄 **CQRS with MediatR**: Implements the Command Query Responsibility Segregation pattern using MediatR.
- ✅ **Fluent Validation**: Ensures robust validation for input models.
- ✂️ **AutoMapper**: Simplifies object-to-object mapping.
- ☁️ **Azure Blob Storage Integration**: Supports file storage with Azure Storage Blobs.
- 🔑 **JWT Authentication**: Provides secure authentication using JWT bearer tokens.
- 🔐 **ASP.NET Core Identity Mechanism**: Offers a comprehensive identity management system for user authentication and role-based authorization.
- 🌍 **Login with Facebook or Google**: Provides seamless integration with external login providers to enhance user experience.
- 📌 **Logging with Serilog**: Structured logging for easier debugging and tracking.
- 🔍 **Visualization with Seq**: Centralized logging visualization for enhanced observability.
- 📜 **Swagger UI**: Includes interactive API documentation with Swashbuckle.AspNetCore.

---

## 🛠️ Getting Started

### Prerequisites

Before setting up the project, ensure you have the following installed:

- ⚙️ [.NET 8 SDK](https://dotnet.microsoft.com/download)
- 🖥️ [Visual Studio 2022](https://visualstudio.microsoft.com/)
- 🐘 [PostgreSQL](https://www.postgresql.org/download/)
- ☁️ Azure account (optional, for Blob Storage integration)
- 🔎 [Seq](https://datalust.co/seq) (optional, for log visualization)

---

### 📥 Installation

Follow these steps to get the solution up and running:

1. **Clone the Repository**  
   ```bash
   git clone https://github.com/adarSonmez/CleanArchitectureTemplate.git
   ```

2. **Navigate to the Solution Directory**  
   ```bash
   cd CleanArchitectureTemplate
   ```

3. **Restore NuGet Packages**  
   ```bash
   dotnet restore
   ```

4. **Configure the Database Connection**  
   Update the `ConnectionStrings:DefaultConnection` in `appsettings.json` located in  
   `Presentation/CleanArchitectureTemplate.WebAPI` with your PostgreSQL details.

5. **Apply Migrations**  
   ```bash
   dotnet ef database update --project Infrastructure/CleanArchitectureTemplate.Persistence
   ```

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

7. **Configure Azure, AWS, or Google Cloud Storage (Optional)**  
   Update the `Storage` section in `appsettings.json` with your provider details. Example:
   ```json
   "Storage": {
       "Azure": "<YourAzureBlobConnectionString>",
       "AWS": "<YourAWSStorageKey>",
       "Google": "<YourGoogleCloudStorageKey>"
   }
   ```

8. **Set Up External Login Providers (Optional)**  
   Update the `OAuth:Google` and `OAuth:Facebook` sections in `appsettings.json` with the appropriate client ID and secret for Google and Facebook login.

9. **Configure JWT Authentication**  
   Ensure the `Jwt` section in `appsettings.json` contains the necessary values for your application's secret key, issuer, audience, and token expiration times. Example:
   ```json
   "Jwt": {
       "SecretKey": "PANTA RHEI!",
       "Issuer": "<YourIssuer>",
       "Audience": "<YourAudience>",
       "AccessTokenExpiration": 360,
       "RefreshTokenExpiration": 60
   }
   ```

---

### 🚀 Usage

1. **Run the Application**  
   ```bash
   dotnet run --project Presentation/CleanArchitectureTemplate.WebAPI
   ```

2. Open your browser and navigate to:  
   🌐 `http://localhost:5000/swagger` to access the Swagger UI for API documentation.

3. View application logs in Seq by navigating to:  
   🌐 `http://localhost:5341` (or your configured Seq server URL).

---

## 🏗️ Project Structure

The solution is organized into the following layers:

- **🎨 Presentation Layer**  
  - `CleanArchitectureTemplate.WebAPI`: The ASP.NET Core Web API project serving as the entry point.

- **🧠 Application Layer**  
  - `CleanArchitectureTemplate.Application`: Contains business logic, DTOs, commands, queries, and validators.

- **📦 Domain Layer**  
  - `CleanArchitectureTemplate.Domain`: Defines domain entities, value objects, and interfaces.

- **🔌 Infrastructure Layer**  
  - `CleanArchitectureTemplate.Infrastructure`: Implements external services like file storage, email sending, and authentication.

- **💾 Persistence Layer**  
  - `CleanArchitectureTemplate.Persistence`: Manages data access with Entity Framework Core, including database contexts and repositories.

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch for your feature:  
   ```bash
   git checkout -b feature/YourFeature
   ```

3. Commit your changes:  
   ```bash
   git commit -m "Add YourFeature"
   ```

4. Push to the branch:  
   ```bash
   git push origin feature/YourFeature
   ```

5. Open a pull request.

✅ **Pro Tip**: Ensure your code follows the project's coding standards and includes appropriate tests.

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

