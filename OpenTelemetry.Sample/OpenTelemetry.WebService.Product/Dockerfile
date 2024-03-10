FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OpenTelemetry.WebService.Product/OpenTelemetry.WebService.Product.csproj", "OpenTelemetry.WebService.Product/"]
RUN dotnet restore "OpenTelemetry.WebService.Product/OpenTelemetry.WebService.Product.csproj"
COPY . .
WORKDIR "/src/OpenTelemetry.WebService.Product"
RUN dotnet build "OpenTelemetry.WebService.Product.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OpenTelemetry.WebService.Product.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenTelemetry.WebService.Product.dll"]
