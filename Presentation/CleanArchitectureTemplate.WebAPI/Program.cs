using CleanArchitectureTemplate.AI;
using CleanArchitectureTemplate.Application;
using CleanArchitectureTemplate.Application.Extensions;
using CleanArchitectureTemplate.Infrastructure;
using CleanArchitectureTemplate.Infrastructure.Filters;
using CleanArchitectureTemplate.Infrastructure.Services.Storage.Local;
using CleanArchitectureTemplate.Messaging;
using CleanArchitectureTemplate.Persistence;
using CleanArchitectureTemplate.RealtimeCommunication;
using CleanArchitectureTemplate.WebAPI.Configurations;
using CleanArchitectureTemplate.WebAPI.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers with Validation Filter
builder.Services
    .AddControllers(o => o.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

// Add Fluent Validation support
builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining(typeof(CleanArchitectureTemplate.Application.ServiceRegistration));

// Register services from other layers
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration)
    .AddInfrastructureServices()
    .AddMessagingServices()
    .AddRealTimeCommunicationServices()
    .AddAIServices(builder.Configuration);

// Configure caching
builder.Services
    .AddResponseCaching(x => x.MaximumBodySize = 1024)
    .AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
    });

// Register storage service (Local Storage)
builder.Services.AddStorage<LocalStorage>();

// Configure CORS to allow requests from any origin, method, and header
builder.Services.AddCors(CorsConfiguration.ConfigureCors);

// Configure Serilog for logging
builder.Host.UseSerilog(LoggingConfiguration.ConfigureSerilog);
builder.Services.AddHttpLogging(LoggingConfiguration.ConfigureHttpLogging);

// Add support for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure authentication
builder.Services
    .AddAuthentication(AuthConfiguration.ConfigureAuthentication)
    .AddJwtBearer(options => AuthConfiguration.ConfigureJwtBearer(options, builder.Services));

// Configure authorization
builder.Services.AddAuthorization(AuthConfiguration.ConfigureAuthorization);

var app = builder.Build();

// Apply any pending migrations automatically
await app.UseMigratorAsync();

// In development mode, enable Swagger UI and seed the database
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.UseSeederAsync();
}

app.UseGlobalExceptionHandler();

app.UseStaticFiles();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseCors();

app.UseContentSecurityPolicy();

app.UseAuthentication();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.MapHubs();

app.UseLogging();

app.Run();