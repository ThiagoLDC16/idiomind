FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build

WORKDIR /src

COPY backend/Idiomind.sln backend/
COPY backend/src/Idiomind.Api/Idiomind.Api.csproj backend/src/Idiomind.Api/
COPY backend/src/Idiomind.Application/Idiomind.Application.csproj backend/src/Idiomind.Application/
COPY backend/src/Idiomind.Domain/Idiomind.Domain.csproj backend/src/Idiomind.Domain/
COPY backend/src/Idiomind.Infrastructure/Idiomind.Infrastructure.csproj backend/src/Idiomind.Infrastructure/

RUN dotnet restore backend/Idiomind.sln

COPY backend/ backend/

RUN dotnet publish backend/src/Idiomind.Api/Idiomind.Api.csproj \
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

ENTRYPOINT ["dotnet", "Idiomind.Api.dll"]
