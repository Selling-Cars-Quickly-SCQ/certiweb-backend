# Fase de ejecución: usa el runtime de ASP.NET 9.0
# CAMBIO: Usar la imagen base de .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5117
EXPOSE 5118

# Fase de compilación: usa el SDK (kit de desarrollo) de .NET 9.0
# CAMBIO: Usar la imagen del SDK de .NET 9
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CertiWeb.API.csproj", "."]
# Puede que necesites restaurar paquetes de feeds específicos para previews
RUN dotnet restore "./CertiWeb.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CertiWeb.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CertiWeb.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CertiWeb.API.dll"]