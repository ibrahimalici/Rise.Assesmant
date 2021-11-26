#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Reports/ReportsAPI/ReportsAPI.csproj", "Services/Reports/ReportsAPI/"]
COPY ["Services/Shareds/SharedLibrary/SharedLibrary.csproj", "Services/Shareds/SharedLibrary/"]
RUN dotnet restore "Services/Reports/ReportsAPI/ReportsAPI.csproj"
COPY . .
WORKDIR "/src/Services/Reports/ReportsAPI"
RUN dotnet build "ReportsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportsAPI.dll"]