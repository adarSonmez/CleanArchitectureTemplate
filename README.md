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
- 📜 **Swagger UI**: Includes interactive API documentation with Swashbuckle.AspNetCore.

---

## 🛠️ Getting Started

### Prerequisites

Before setting up the project, ensure you have the following installed:

- ⚙️ [.NET 8 SDK](https://dotnet.microsoft.com/download)
- 🖥️ [Visual Studio 2022](https://visualstudio.microsoft.com/)
- 🐘 [PostgreSQL](https://www.postgresql.org/download/)
- ☁️ Azure account (optional, for Blob Storage integration)

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
   Update the `ConnectionStrings` in `appsettings.json` located in  
   `Presentation/CleanArchitectureTemplate.WebAPI` with your PostgreSQL details.

5. **Apply Migrations**  
   ```bash
   dotnet ef database update --project Infrastructure/CleanArchitectureTemplate.Persistence
   ```

6. **Configure Azure Services (Optional)**  
   If you intend to use Azure Blob Storage, update the Azure storage configurations in `appsettings.json`.

---

### 🚀 Usage

1. **Run the Application**  
   ```bash
   dotnet run --project Presentation/CleanArchitectureTemplate.WebAPI
   ```

2. Open your browser and navigate to:  
   🌐 `http://localhost:5000/swagger` to access the Swagger UI for API documentation.

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
