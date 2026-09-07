FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build

WORKDIR /src

COPY backend/Cognilingo.sln backend/
COPY backend/src/Cognilingo.Api/Cognilingo.Api.csproj backend/src/Cognilingo.Api/
COPY backend/src/Cognilingo.Application/Cognilingo.Application.csproj backend/src/Cognilingo.Application/
COPY backend/src/Cognilingo.Domain/Cognilingo.Domain.csproj backend/src/Cognilingo.Domain/
COPY backend/src/Cognilingo.Infrastructure/Cognilingo.Infrastructure.csproj backend/src/Cognilingo.Infrastructure/

RUN dotnet restore backend/Cognilingo.sln

COPY backend/ backend/

RUN dotnet publish backend/src/Cognilingo.Api/Cognilingo.Api.csproj \
    --configuration Release \
    --output /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled-extra AS final

WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=5172
EXPOSE 5172

COPY --from=build /app/publish .

USER $APP_UID

ENTRYPOINT ["dotnet", "Cognilingo.Api.dll"]
