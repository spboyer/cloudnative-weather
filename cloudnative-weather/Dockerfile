FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build
WORKDIR /src
COPY ["cloudnative-weather/cloudnative-weather.csproj", "cloudnative-weather/"]
RUN dotnet restore "cloudnative-weather/cloudnative-weather.csproj"
COPY . .
WORKDIR "/src/cloudnative-weather"
RUN dotnet build "cloudnative-weather.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cloudnative-weather.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cloudnative-weather.dll"]