FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80

EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /src

COPY ["GamesWebApi.Presentation/GamesWebApi.Presentation.csproj", "GamesWebApi.Presentation/"]

COPY ["Application/Application.csproj", "Application/"]

COPY ["Domain/Domain.csproj", "Domain/"]

COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "GamesWebApi.Presentation/GamesWebApi.Presentation.csproj"

COPY . .

WORKDIR "/src/GamesWebApi.Presentation"

RUN dotnet build "GamesWebApi.Presentation.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "GamesWebApi.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GamesWebApi.Presentation.dll"]
