#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bandit.NSI.Daemon/Bandit.NSI.Daemon.csproj", "src/Bandit.NSI.Daemon/"]
COPY ["src/Bandit.NSI.AuthNpgsqlRepository/Bandit.NSI.AuthNpgsqlRepository.csproj", "src/Bandit.NSI.AuthNpgsqlRepository/"]
COPY ["src/Bandit.NSI.DecisNpgsqlRepository/Bandit.NSI.DecisNpgsqlRepository.csproj", "src/Bandit.NSI.DecisNpgsqlRepository/"]
COPY ["src/Bandit.NSI.TempNpgsqlRepository/Bandit.NSI.TempNpgsqlRepository.csproj", "src/Bandit.NSI.TempNpgsqlRepository/"]
RUN dotnet restore "src/Bandit.NSI.Daemon/Bandit.NSI.Daemon.csproj"
COPY . .
WORKDIR "/src/src/Bandit.NSI.Daemon"
RUN dotnet build "Bandit.NSI.Daemon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bandit.NSI.Daemon.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bandit.NSI.Daemon.dll"]