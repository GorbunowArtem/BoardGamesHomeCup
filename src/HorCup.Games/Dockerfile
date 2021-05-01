FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY ["src/HorCup.Games/HorCup.Games.csproj", "src/HorCup.Games/"]
COPY ["src/HorCup.Infrastructure/HorCup.Infrastructure.csproj", "src/HorCup.Infrastructure/"]
RUN dotnet restore "src/HorCup.Games/HorCup.Games.csproj"
COPY . .
WORKDIR "/src/src/HorCup.Games"
RUN dotnet build "HorCup.Games.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HorCup.Games.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HorCup.Games.dll"]
