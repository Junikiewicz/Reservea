FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

# Restore nuget packages for service
COPY "Reservea.Microservices/Reservea.Microservices.Reservations/Reservea.Microservices.Reservations.csproj" "Reservea.Microservices/Reservea.Microservices.Reservations/"
COPY "Reservea.Common/Reservea.Common.csproj" "Reservea.Common/"
RUN dotnet restore "Reservea.Microservices/Reservea.Microservices.Reservations/Reservea.Microservices.Reservations.csproj"

# Restore nuget packages for tests
COPY "Reservea.Tests/Reservea.Tests.Reservations/Reservea.Tests.Reservations.csproj" "Reservea.Tests/Reservea.Tests.Reservations/"
COPY "Reservea.Tests/Reservea.Tests.Common/Reservea.Tests.Common.csproj" "Reservea.Tests/Reservea.Tests.Common/"
COPY "Reservea.Persistance/Reservea.Persistance.csproj" "Reservea.Persistance/"
RUN dotnet restore "Reservea.Tests/Reservea.Tests.Reservations/Reservea.Tests.Reservations.csproj"
RUN dotnet restore "Reservea.Tests/Reservea.Tests.Common/Reservea.Tests.Common.csproj"

# Copy service source files
COPY "Reservea.Microservices/Reservea.Microservices.Reservations/" "Reservea.Microservices/Reservea.Microservices.Reservations/"
COPY "Reservea.Common/" "Reservea.Common/"
COPY "Reservea.Persistance/" "Reservea.Persistance/"

# Docker build bug in CI environment workaround
RUN true

# Copy test files
COPY "Reservea.Tests/Reservea.Tests.Reservations/" "Reservea.Tests/Reservea.Tests.Reservations/"
COPY "Reservea.Tests/Reservea.Tests.Common/" "Reservea.Tests/Reservea.Tests.Common/"

# Build
WORKDIR /
RUN dotnet build "src/Reservea.Microservices/Reservea.Microservices.Reservations/Reservea.Microservices.Reservations.csproj" -c Release -o app/build --no-restore

# Test
RUN dotnet test "src/Reservea.Tests/Reservea.Tests.Reservations" -c Release --no-restore
RUN dotnet test "src/Reservea.Tests/Reservea.Tests.Common" -c Release --no-restore

# Publish
FROM build AS publish
RUN dotnet publish "src/Reservea.Microservices/Reservea.Microservices.Reservations/Reservea.Microservices.Reservations.csproj" -c Release -o app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish app/publish .
ENTRYPOINT ["dotnet", "Reservea.Microservices.Reservations.dll"]