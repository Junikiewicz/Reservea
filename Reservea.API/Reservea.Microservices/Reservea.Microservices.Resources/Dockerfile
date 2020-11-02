FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

# Restore nuget packages for service
COPY "Microservices/Reservea.Microservices.Resources/Reservea.Microservices.Resources.csproj" "Microservices/Reservea.Microservices.Resources"
COPY "Reservea.Common/Reservea.Common.csproj" "Reservea.Common"
RUN dotnet restore "Microservices/Reservea.Microservices.Resources/Reservea.Microservices.Resources.csproj"

# Restore nuget packages for tests
COPY "Tests/Reservea.Tests.Resources/Reservea.Tests.Resources.csproj" "Tests/Reservea.Tests.Resources/"
COPY "Tests/Reservea.Tests.Common/Reservea.Tests.Common.csproj" "Tests/Reservea.Tests.Common/"
RUN dotnet restore "Tests/Reservea.Tests.Resources/Reservea.Tests.Resources.csproj"
RUN dotnet restore "Tests/Reservea.Tests.Common/Reservea.Tests.Common.csproj"

# Copy service source files
COPY "Microservices/Reservea.Microservices.Resources/" "Microservices/Reservea.Microservices.Resources/"
COPY "Reservea.Common/" "Reservea.Common/"

# Copy test files
COPY "Tests/Reservea.Tests.Resources/" "Tests/Reservea.Tests.Resources/"
COPY "Tests/Reservea.Tests.Common/" "Tests/Reservea.Tests.Common/"

# Build
WORKDIR /
RUN dotnet build "src/Microservices/Reservea.Microservices.Resources/Reservea.Microservices.Resources.csproj" -c Release -o app/build --no-restore

# Test
RUN dotnet test "src/Tests/Reservea.Tests.Resources" -c Release --no-restore
RUN dotnet test "src/Tests/Reservea.Tests.Common" -c Release --no-restore

# Publish
FROM build AS publish
RUN dotnet publish "src/Microservices/Reservea.Microservices.Resources/Reservea.Microservices.Resources.csproj" -c Release -o app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish app/publish .
ENTRYPOINT ["dotnet", "Reservea.Microservices.Resources.dll"]