FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Prevent 'Warning: apt-key output should not be parsed (stdout is not a terminal)'
ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=1



FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

# install NodeJS 14.x
# see https://github.com/nodesource/distributions/blob/master/README.md#deb
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs


#COPY "src/HorCup.Presentation/HorCup.Presentation.csproj" "src/HorCup.Presentation/HorCup.Presentation.csproj"

#COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "/root/.nuget/NuGet.config"

COPY . .
WORKDIR /src/src/HorCup.Presentation

FROM build AS publish
RUN dotnet publish "HorCup.Presentation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HorCup.Presentation.dll"]
