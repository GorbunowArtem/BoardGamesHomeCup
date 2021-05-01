FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY ["src/HorCup.Players/HorCup.Players.csproj", "src/HorCup.Players/"]
COPY ["src/HorCup.Infrastructure/HorCup.Infrastructure.csproj", "src/HorCup.Infrastructure/"]
RUN dotnet restore "src/HorCup.Players/HorCup.Players.csproj"
COPY . .
WORKDIR "/src/src/HorCup.Players"
RUN dotnet build "HorCup.Players.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HorCup.Players.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HorCup.Players.dll"]
