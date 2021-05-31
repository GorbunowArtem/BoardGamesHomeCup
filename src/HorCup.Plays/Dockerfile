FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY ["src/HorCup.Plays/HorCup.Plays.csproj", "src/HorCup.Plays/"]
COPY ["src/HorCup.Infrastructure/HorCup.Infrastructure.csproj", "src/HorCup.Infrastructure/"]
RUN dotnet restore "src/HorCup.Plays/HorCup.Plays.csproj"
COPY . .
WORKDIR "/src/src/HorCup.Plays"
RUN dotnet build "HorCup.Plays.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HorCup.Plays.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HorCup.Plays.dll"]
