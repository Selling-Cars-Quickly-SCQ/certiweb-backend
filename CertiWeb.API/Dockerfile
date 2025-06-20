# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This phase is used when running from VS in quick mode (default for debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5117
EXPOSE 5118


# This phase is used to compile the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CertiWeb.API.csproj", "."]
RUN dotnet restore "./CertiWeb.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CertiWeb.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This phase is used to publish the service project that will be copied in the final phase.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CertiWeb.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This phase is used in production or when running from VS in normal mode (default when not using debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CertiWeb.API.dll"]