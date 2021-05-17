FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["src/HorCup.Statistic/HorCup.Statistic.csproj", "src/HorCup.Statistic/"]
COPY ["src/HorCup.Infrastructure/HorCup.Infrastructure.csproj", "src/HorCup.Infrastructure/"]
RUN dotnet restore "src/HorCup.Statistic/HorCup.Statistic.csproj"
COPY . .
WORKDIR "/src/src/HorCup.Statistic"
RUN dotnet build "HorCup.Statistic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HorCup.Statistic.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HorCup.Statistic.dll"]
