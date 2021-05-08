#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:16-alpine3.11 as build-node
WORKDIR /ClientApp
COPY src/HorCup.Presentation/ClientApp/package.json .
COPY src/HorCup.Presentation/ClientApp/package-lock.json .
RUN npm install
COPY src/HorCup.Presentation/ClientApp/ . 
RUN npm run build  

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY ["src/HorCup.Presentation/HorCup.Presentation.csproj", "src/HorCup.Presentation/"]
COPY ["src/HorCup.Infrastructure/HorCup.Infrastructure.csproj", "src/HorCup.Infrastructure/"]
RUN dotnet restore "src/HorCup.Presentation/HorCup.Presentation.csproj"
COPY . .
WORKDIR "/src/src/HorCup.Presentation"
RUN dotnet build "HorCup.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HorCup.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-node /ClientApp ./ClientApp
ENTRYPOINT ["dotnet", "HorCup.Presentation.dll"]
