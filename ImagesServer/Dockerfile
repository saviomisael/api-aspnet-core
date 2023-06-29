FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80

EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /src

COPY ["ImagesServer.Presentation/ImagesServer.Presentation.csproj", "ImagesServer.Presentation/"]

COPY ["Application/Application.csproj", "Application/"]

COPY ["Domain/Domain.csproj", "Domain/"]

COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "ImagesServer.Presentation/ImagesServer.Presentation.csproj"

COPY . .

WORKDIR "/src/ImagesServer.Presentation"

RUN dotnet build "ImagesServer.Presentation.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "ImagesServer.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ImagesServer.Presentation.dll"]