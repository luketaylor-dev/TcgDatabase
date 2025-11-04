FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Yu-Gi-Oh-Database/Yu-Gi-Oh-Database.csproj", "Yu-Gi-Oh-Database/"]
RUN dotnet restore "Yu-Gi-Oh-Database/Yu-Gi-Oh-Database.csproj"
COPY . .
WORKDIR "/src/Yu-Gi-Oh-Database"
RUN dotnet build "Yu-Gi-Oh-Database.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yu-Gi-Oh-Database.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yu-Gi-Oh-Database.dll"]
