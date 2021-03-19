#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/HorCup.IdentityServer/HorCup.IdentityServer.csproj", "src/HorCup.IdentityServer/"]
COPY ["src/HorCup.Infrastructure/HorCup.Infrastructure.csproj", "src/HorCup.Infrastructure/"]
RUN dotnet restore "src/HorCup.IdentityServer/HorCup.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/HorCup.IdentityServer"
RUN dotnet build "HorCup.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HorCup.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HorCup.IdentityServer.dll"]