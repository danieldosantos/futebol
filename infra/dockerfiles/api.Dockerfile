FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src
COPY apps/api/BolaSocial.Api.csproj apps/api/
RUN dotnet restore apps/api/BolaSocial.Api.csproj
COPY . .
WORKDIR /src/apps/api
RUN dotnet publish BolaSocial.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BolaSocial.Api.dll"]
