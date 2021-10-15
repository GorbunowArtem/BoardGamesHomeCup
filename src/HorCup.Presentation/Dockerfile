ARG NODE_IMAGE=node:16.0
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM ${NODE_IMAGE} as node-build
WORKDIR /web
COPY src/HorCup.Presentation/ClientApp/package.json .
COPY src/HorCup.Presentation/ClientApp/package-lock.json .
RUN npm install -g npm@6
RUN npm install
COPY src/HorCup.Presentation/ClientApp .
RUN npm run build:prod

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

#COPY "src/HorCup.Presentation/HorCup.Presentation.csproj" "src/HorCup.Presentation/HorCup.Presentation.csproj"

#COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "/root/.nuget/NuGet.config"

COPY --from=node-build /web/ /src/src/HorCup.Presentation/ClientApp/

COPY . .
WORKDIR /src/src/HorCup.Presentation

FROM build AS publish
RUN dotnet publish "HorCup.Presentation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HorCup.Presentation.dll"]
