FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

# Restore nuget packages for service
COPY "Microservices/Reservea.Microservices.Reservations/Reservea.Microservices.Reservations.csproj" "Microservices/Reservea.Microservices.Reservations"
COPY "Reservea.Common/Reservea.Common.csproj" "Reservea.Common"
RUN dotnet restore "Microservices/Reservea.Microservices.Reservations/Reservea.Microservices.Reservations.csproj"

# Restore nuget packages for tests
COPY "Tests/Reservea.Tests.Reservations/Reservea.Tests.Reservations.csproj" "Tests/Reservea.Tests.Reservations/"
COPY "Tests/Reservea.Tests.Common/Reservea.Tests.Common.csproj" "Tests/Reservea.Tests.Common/"
RUN dotnet restore "Tests/Reservea.Tests.Reservations/Reservea.Tests.Reservations.csproj"
RUN dotnet restore "Tests/Reservea.Tests.Common/Reservea.Tests.Common.csproj"

# Copy service source files
COPY "Microservices/Reservea.Microservices.Reservations/" "Microservices/Reservea.Microservices.Reservations/"
COPY "Reservea.Common/" "Reservea.Common/"

# Copy test files
COPY "Tests/Reservea.Tests.Reservations/" "Tests/Reservea.Tests.Reservations/"
COPY "Tests/Reservea.Tests.Common/" "Tests/Reservea.Tests.Common/"

# Build
WORKDIR /
RUN dotnet build "src/Microservices/Reservea.Microservices.Reservations/Reservea.Microservices.Reservations.csproj" -c Release -o app/build --no-restore

# Test
RUN dotnet test "src/Tests/Reservea.Tests.Reservations" -c Release --no-restore
RUN dotnet test "src/Tests/Reservea.Tests.Common" -c Release --no-restore

# Publish
FROM build AS publish
RUN dotnet publish "src/Microservices/Reservea.Microservices.Reservations/Reservea.Microservices.Reservations.csproj" -c Release -o app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish app/publish .
ENTRYPOINT ["dotnet", "Reservea.Microservices.Reservations.dll"]