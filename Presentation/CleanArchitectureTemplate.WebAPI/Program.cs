using CleanArchitectureTemplate.Application;
using CleanArchitectureTemplate.Application.Extensions;
using CleanArchitectureTemplate.Infrastructure;
using CleanArchitectureTemplate.Infrastructure.Filters;
using CleanArchitectureTemplate.Infrastructure.Services.Storage.Local;
using CleanArchitectureTemplate.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(o => o.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CleanArchitectureTemplate.Application.ServiceRegistration));

// Register services from other layers
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddStorage<LocalStorage>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

app.UseMigrator();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.UseSeederAsync();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();