FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ClusterConnect.csproj", "./"]
RUN dotnet restore "ClusterConnect.csproj"
COPY . .
RUN dotnet build "ClusterConnect.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClusterConnect.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClusterConnect.dll"]
