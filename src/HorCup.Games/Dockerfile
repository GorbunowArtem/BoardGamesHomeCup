FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY "HorCup.sln" "HorCup.sln"
COPY "src/HorCup.Games/HorCup.Games.csproj" "HorCup.Games/HorCup.Games.csproj"
COPY "src/HorCup.IdentityServer/HorCup.IdentityServer.csproj" "HorCup.IdentityServer/HorCup.IdentityServer.csproj"
COPY "src/HorCup.Infrastructure/HorCup.Infrastructure.csproj" "HorCup.Infrastructure/HorCup.Infrastructure.csproj"
COPY "src/HorCup.Infrastructure/HorCup.Infrastructure.csproj" "HorCup.Infrastructure/HorCup.Infrastructure.csproj"
COPY "src/HorCup.Players/HorCup.Players.csproj" "HorCup.Players/HorCup.Players.csproj"
COPY "src/HorCup.Plays/HorCup.Plays.csproj" "HorCup.Plays/HorCup.Plays.csproj"
COPY "src/HorCup.Plays.Shared/HorCup.Plays.Shared.csproj" "HorCup.Plays.Shared/HorCup.Plays.Shared.csproj"
COPY "src/HorCup.Presentation/HorCup.Presentation.csproj" "HorCup.Presentation/HorCup.Presentation.csproj"
COPY "src/HorCup.Statistic/HorCup.Statistic.csproj" "HorCup.Statistic/HorCup.Statistic.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "/root/.nuget/NuGet.config"

RUN dotnet restore "HorCup.sln"

COPY . .

WORKDIR "/src/HorCup.Games"

FROM build AS publish
RUN dotnet publish "HorCup.Games.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HorCup.Games.dll"]
