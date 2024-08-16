#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["THY.GatePlanner.API/THY.GatePlanner.API.csproj", "THY.GatePlanner.API/"]
COPY ["THY.GatePlanner.Infrastructure/THY.GatePlanner.Infrastructure.csproj", "THY.GatePlanner.Infrastructure/"]
COPY ["THY.GatePlanner.Model/THY.GatePlanner.Model.csproj", "THY.GatePlanner.Model/"]
COPY ["THY.GatePlanner.Service/THY.GatePlanner.Service.csproj", "THY.GatePlanner.Service/"]
RUN dotnet restore "./THY.GatePlanner.API/./THY.GatePlanner.API.csproj"
COPY . .
WORKDIR "/src/THY.GatePlanner.API"
RUN dotnet build "./THY.GatePlanner.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./THY.GatePlanner.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "THY.GatePlanner.API.dll"]