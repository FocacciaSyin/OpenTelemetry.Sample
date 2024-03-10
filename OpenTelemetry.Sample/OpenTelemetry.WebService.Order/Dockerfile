FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OpenTelemetry.WebService.Order/OpenTelemetry.WebService.Order.csproj", "OpenTelemetry.WebService.Order/"]
RUN dotnet restore "OpenTelemetry.WebService.Order/OpenTelemetry.WebService.Order.csproj"
COPY . .
WORKDIR "/src/OpenTelemetry.WebService.Order"
RUN dotnet build "OpenTelemetry.WebService.Order.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OpenTelemetry.WebService.Order.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenTelemetry.WebService.Order.dll"]
