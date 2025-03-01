FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY Directory.Packages.props Directory.Packages.props

COPY ./src ./

RUN dotnet restore

COPY ./src .

RUN dotnet build WeatherForecast.WebApi/WeatherForecast.WebApi.csproj --configuration Release --output /app/build
RUN dotnet publish WeatherForecast.WebApi/WeatherForecast.WebApi.csproj --configuration Release --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS http://*:5080
EXPOSE 5080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WeatherForecast.WebApi.dll"]