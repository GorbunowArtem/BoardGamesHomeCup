ARG NODE_IMAGE=node:16.0
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM ${NODE_IMAGE} as node-build
WORKDIR /web/src
COPY src/HorCup.Presentation/ClientApp/package.json .
COPY src/HorCup.Presentation/ClientApp/package-lock.json .
RUN npm install -g npm@7.15.1
RUN npm install
COPY src/HorCup.Presentation/ClientApp/ .
RUN npm npm build:prod

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY "HorCup.sln" "HorCup.sln"
COPY "src/HorCup.Games/HorCup.Games.csproj" "src/HorCup.Games/HorCup.Games.csproj"
COPY "src/HorCup.IdentityServer/HorCup.IdentityServer.csproj" "src/HorCup.IdentityServer/HorCup.IdentityServer.csproj"
COPY "src/HorCup.Infrastructure/HorCup.Infrastructure.csproj" "src/HorCup.Infrastructure/HorCup.Infrastructure.csproj"
COPY "src/HorCup.Infrastructure/HorCup.Infrastructure.csproj" "src/HorCup.Infrastructure/HorCup.Infrastructure.csproj"
COPY "src/HorCup.Players/HorCup.Players.csproj" "src/HorCup.Players/HorCup.Players.csproj"
COPY "src/HorCup.Plays/HorCup.Plays.csproj" "src/HorCup.Plays/HorCup.Plays.csproj"
COPY "src/HorCup.Plays.Shared/HorCup.Plays.Shared.csproj" "src/HorCup.Plays.Shared/HorCup.Plays.Shared.csproj"
COPY "src/HorCup.Presentation/HorCup.Presentation.csproj" "src/HorCup.Presentation/HorCup.Presentation.csproj"
COPY "src/HorCup.Statistic/HorCup.Statistic.csproj" "src/HorCup.Statistic/HorCup.Statistic.csproj"

COPY "tests/HorCup.Games.Tests/HorCup.Games.Tests.csproj" "tests/HorCup.Games.Tests/HorCup.Games.Tests.csproj"
COPY "tests/HorCup.Players.Tests/HorCup.Players.Tests.csproj" "tests/HorCup.Players.Tests/HorCup.Players.Tests.csproj"
COPY "tests/HorCup.Plays.Tests/HorCup.Plays.Tests.csproj" "tests/HorCup.Plays.Tests/HorCup.Plays.Tests.csproj"
COPY "tests/HorCup.Tests.Base/HorCup.Tests.Base.csproj" "tests/HorCup.Tests.Base/HorCup.Tests.Base.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "/root/.nuget/NuGet.config"

RUN dotnet restore "HorCup.sln"

COPY . .
WORKDIR /src/src/HorCup.Presentation

FROM build AS publish
RUN dotnet publish "HorCup.Presentation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HorCup.Presentation.dll"]
