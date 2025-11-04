FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TcgDatabase.API/TcgDatabase.API.csproj", "TcgDatabase.API/"]
COPY ["TcgDatabase.Application/TcgDatabase.Application.csproj", "TcgDatabase.Application/"]
COPY ["TcgDatabase.Domain/TcgDatabase.Domain.csproj", "TcgDatabase.Domain/"]
COPY ["TcgDatabase.Infrastructure/TcgDatabase.Infrastructure.csproj", "TcgDatabase.Infrastructure/"]

RUN dotnet restore "TcgDatabase.API/TcgDatabase.API.csproj"
COPY . .
WORKDIR "/src/TcgDatabase.API"
RUN dotnet build "TcgDatabase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TcgDatabase.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TcgDatabase.API.dll"]

