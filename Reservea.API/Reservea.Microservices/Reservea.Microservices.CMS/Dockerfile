FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Restore nuget packages for service
COPY "Reservea.Microservices/Reservea.Microservices.CMS/Reservea.Microservices.CMS.csproj" "Reservea.Microservices/Reservea.Microservices.CMS/"
COPY "Reservea.Common/Reservea.Common.csproj" "Reservea.Common/"
COPY "Reservea.Persistance/Reservea.Persistance.csproj" "Reservea.Persistance/"
RUN dotnet restore "Reservea.Microservices/Reservea.Microservices.CMS/Reservea.Microservices.CMS.csproj"

# Restore nuget packages for tests
COPY "Reservea.Tests/Reservea.Tests.CMS/Reservea.Tests.CMS.csproj" "Reservea.Tests/Reservea.Tests.CMS/"
COPY "Reservea.Tests/Reservea.Tests.Common/Reservea.Tests.Common.csproj" "Reservea.Tests/Reservea.Tests.Common/"
RUN dotnet restore "Reservea.Tests/Reservea.Tests.CMS/Reservea.Tests.CMS.csproj"
RUN dotnet restore "Reservea.Tests/Reservea.Tests.Common/Reservea.Tests.Common.csproj"

# Copy service source files
COPY "Reservea.Microservices/Reservea.Microservices.CMS/" "Reservea.Microservices/Reservea.Microservices.CMS/"
COPY "Reservea.Common/" "Reservea.Common/"
COPY "Reservea.Persistance/" "Reservea.Persistance/"

# Docker build bug in CI environment workaround
RUN true

# Copy test files
COPY "Reservea.Tests/Reservea.Tests.CMS/" "Reservea.Tests/Reservea.Tests.CMS/"
COPY "Reservea.Tests/Reservea.Tests.Common/" "Reservea.Tests/Reservea.Tests.Common/"

# Build
WORKDIR /
RUN dotnet build "src/Reservea.Microservices/Reservea.Microservices.CMS/Reservea.Microservices.CMS.csproj" -c Release -o app/build --no-restore

# Test
RUN dotnet test "src/Reservea.Tests/Reservea.Tests.CMS" -c Release --no-restore
RUN dotnet test "src/Reservea.Tests/Reservea.Tests.Common" -c Release --no-restore

# Publish
FROM build AS publish
RUN dotnet publish "src/Reservea.Microservices/Reservea.Microservices.CMS/Reservea.Microservices.CMS.csproj" -c Release -o app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish app/publish .
ENTRYPOINT ["dotnet", "Reservea.Microservices.CMS.dll"]