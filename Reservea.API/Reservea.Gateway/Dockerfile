FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Restore nuget packages for service
COPY "Reservea.Gateway/Reservea.Gateway.csproj" "Reservea.Gateway/"
RUN dotnet restore "Reservea.Gateway/Reservea.Gateway.csproj"

# Copy service source files
COPY "Reservea.Gateway/" "Reservea.Gateway/"

# Build
WORKDIR /
RUN dotnet build "src/Reservea.Gateway/Reservea.Gateway.csproj" -c Release -o app/build --no-restore

# Publish
FROM build AS publish
RUN dotnet publish "src/Reservea.Gateway/Reservea.Gateway.csproj" -c Release -o app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish app/publish .
ENTRYPOINT ["dotnet", "Reservea.Gateway.dll"]