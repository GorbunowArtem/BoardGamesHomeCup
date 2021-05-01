FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/HorCup.Players/HorCup.Players.csproj", "HorCup.Players/"]
RUN dotnet restore "src/HorCup.Players/HorCup.Players.csproj"
COPY . .
WORKDIR "/src/HorCup.Players"
RUN dotnet build "HorCup.Players.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HorCup.Players.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HorCup.Players.dll"]
