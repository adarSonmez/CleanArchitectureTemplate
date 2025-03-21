# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
LABEL maintainer="Adar Sönmez"

USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/CleanArchitectureTemplate.WebAPI/CleanArchitectureTemplate.WebAPI.csproj", "Presentation/CleanArchitectureTemplate.WebAPI/"]
COPY ["Core/CleanArchitectureTemplate.Application/CleanArchitectureTemplate.Application.csproj", "Core/CleanArchitectureTemplate.Application/"]
COPY ["Core/CleanArchitectureTemplate.Domain/CleanArchitectureTemplate.Domain.csproj", "Core/CleanArchitectureTemplate.Domain/"]
COPY ["Infrastructure/CleanArchitectureTemplate.Infrastructure/CleanArchitectureTemplate.Infrastructure.csproj", "Infrastructure/CleanArchitectureTemplate.Infrastructure/"]
COPY ["Infrastructure/CleanArchitectureTemplate.Persistence/CleanArchitectureTemplate.Persistence.csproj", "Infrastructure/CleanArchitectureTemplate.Persistence/"]
COPY ["Infrastructure/CleanArchitectureTemplate.SignalR/CleanArchitectureTemplate.SignalR.csproj", "Infrastructure/CleanArchitectureTemplate.SignalR/"]
RUN dotnet restore "./Presentation/CleanArchitectureTemplate.WebAPI/CleanArchitectureTemplate.WebAPI.csproj"
COPY . .
WORKDIR "/src/Presentation/CleanArchitectureTemplate.WebAPI"
RUN dotnet build "./CleanArchitectureTemplate.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArchitectureTemplate.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitectureTemplate.WebAPI.dll"]