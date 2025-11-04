FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY src/Finnova.Domain/Finnova.Domain.csproj src/Finnova.Domain/
COPY src/Finnova.Application/Finnova.Application.csproj src/Finnova.Application/
COPY src/Finnova.Infrastructure/Finnova.Infrastructure.csproj src/Finnova.Infrastructure/
COPY src/Finnova.API/Finnova.API.csproj src/Finnova.API/

RUN dotnet restore src/Finnova.API/Finnova.API.csproj

COPY . .

RUN dotnet publish src/Finnova.API/Finnova.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Finnova.API.dll"]