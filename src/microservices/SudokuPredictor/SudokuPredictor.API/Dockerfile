#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/microservices/SudokuPredictor/SudokuPredictor.API/SudokuPredictor.API.csproj", "src/microservices/SudokuPredictor/SudokuPredictor.API/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.HealthCheck.Dapr/ModuleDistributor.HealthCheck.Dapr.csproj", "src/modules/ModuleDistributor/ModuleDistributor.HealthCheck.Dapr/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Dapr/ModuleDistributor.Dapr.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Dapr/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor/ModuleDistributor.csproj", "src/modules/ModuleDistributor/ModuleDistributor/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.HealthCheck/ModuleDistributor.HealthCheck.csproj", "src/modules/ModuleDistributor/ModuleDistributor.HealthCheck/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Logging/ModuleDistributor.Logging.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Logging/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Serilog/ModuleDistributor.Serilog.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Serilog/"]
COPY ["src/modules/ModuleDistributor/ModuleDistributor.Swagger/ModuleDistributor.Swagger.csproj", "src/modules/ModuleDistributor/ModuleDistributor.Swagger/"]
COPY ["src/modules/Sudoku.Algorithm/Sudoku.Algorithm/Sudoku.Algorithm.csproj", "src/modules/Sudoku.Algorithm/Sudoku.Algorithm/"]
COPY ["src/modules/Sudoku.Algorithm/Sudoku.Algorithm.Abstracts/Sudoku.Algorithm.Abstracts.csproj", "src/modules/Sudoku.Algorithm/Sudoku.Algorithm.Abstracts/"]
COPY ["src/microservices/SudokuFactory/SudokuFactory.Shared/SudokuFactory.Shared.csproj", "src/microservices/SudokuFactory/SudokuFactory.Shared/"]
COPY ["src/microservices/SudokuPredictor/SudokuPredictor.Shared/SudokuPredictor.Shared.csproj", "src/microservices/SudokuPredictor/SudokuPredictor.Shared/"]
RUN dotnet restore "src/microservices/SudokuPredictor/SudokuPredictor.API/SudokuPredictor.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/SudokuPredictor/SudokuPredictor.API"
RUN dotnet build "SudokuPredictor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SudokuPredictor.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SudokuPredictor.API.dll"]