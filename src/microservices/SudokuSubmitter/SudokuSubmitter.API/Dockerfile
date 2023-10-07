#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/microservices/SudokuSubmitter/SudokuSubmitter.API/SudokuSubmitter.API.csproj", "src/microservices/SudokuSubmitter/SudokuSubmitter.API/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.HealthCheck.Dapr/ModuleDistributor.HealthCheck.Dapr.csproj", "src/modules/ModuleDistributor/ModuleDistributor.HealthCheck.Dapr/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Dapr/ModuleDistributor.Dapr.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Dapr/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor/ModuleDistributor.csproj", "src/modules/ModuleDistributor/ModuleDistributor/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.HealthCheck/ModuleDistributor.HealthCheck.csproj", "src/modules/ModuleDistributor/ModuleDistributor.HealthCheck/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Serilog/ModuleDistributor.Serilog.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Serilog/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Swagger/ModuleDistributor.Swagger.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Swagger/"]
COPY ["src/microservices/SudokuSubmitter/SudokuSubmitter.Application/SudokuSubmitter.Application.csproj", "src/microservices/SudokuSubmitter/SudokuSubmitter.Application/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.DependencyInjection/ModuleDistributor.DependencyInjection.csproj", "src/modules/ModuleDistributor/ModuleDistributor.DependencyInjection/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Logging/ModuleDistributor.Logging.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Logging/"]
COPY ["src/microservices/SudokuFactory/SudokuFactory.Shared/SudokuFactory.Shared.csproj", "src/microservices/SudokuFactory/SudokuFactory.Shared/"]
COPY ["src/microservices/SudokuSubmitter/SudokuSubmitter.Application.Abstracts/SudokuSubmitter.Application.Abstracts.csproj", "src/microservices/SudokuSubmitter/SudokuSubmitter.Application.Abstracts/"]
COPY ["src/microservices/SudokuSubmitter/SudokuSubmitter.Domain.Shared/SudokuSubmitter.Domain.Shared.csproj", "src/microservices/SudokuSubmitter/SudokuSubmitter.Domain.Shared/"]
COPY ["src/microservices/SudokuSubmitter/SudokuSubmitter.EntityFrameworkCore/SudokuSubmitter.EntityFrameworkCore.csproj", "src/microservices/SudokuSubmitter/SudokuSubmitter.EntityFrameworkCore/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.EntityFrameworkCore/ModuleDistributor.EntityFrameworkCore.csproj", "src/modules/ModuleDistributor/ModuleDistributor.EntityFrameworkCore/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Repository/ModuleDistributor.Repository.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Repository/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Repository.Abstractions/ModuleDistributor.Repository.Abstractions.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Repository.Abstractions/"]
COPY ["src/microservices/SudokuSubmitter/SudokuSubmitter.Domain/SudokuSubmitter.Domain.csproj", "src/microservices/SudokuSubmitter/SudokuSubmitter.Domain/"]
RUN dotnet restore "src/microservices/SudokuSubmitter/SudokuSubmitter.API/SudokuSubmitter.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/SudokuSubmitter/SudokuSubmitter.API"
RUN dotnet build "SudokuSubmitter.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SudokuSubmitter.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SudokuSubmitter.API.dll"]