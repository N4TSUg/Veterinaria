# Imagen base para el entorno de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# Imagen del SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Veterinaria.csproj", "./"]
RUN dotnet restore "./Veterinaria.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Veterinaria.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Veterinaria.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Veterinaria.dll"]
