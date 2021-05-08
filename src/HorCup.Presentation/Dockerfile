#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

# Prevent 'Warning: apt-key output should not be parsed (stdout is not a terminal)'
ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=1

# install NodeJS 14.x
# see https://github.com/nodesource/distributions/blob/master/README.md#deb
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs

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
ENTRYPOINT ["dotnet", "HorCup.Presentation.dll"]
