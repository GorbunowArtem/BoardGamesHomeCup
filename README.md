# Board Games Home Cup

[![HorCup](https://github.com/GorbunowArtem/BoardGamesHomeCup/workflows/HorCup/badge.svg)](https://github.com/GorbunowArtem/BoardGamesHomeCup/actions)
[![Build status](https://dev.azure.com/artem-horbunov/hor-cup/_apis/build/status/hor-cup-Docker%20container-CI)](https://dev.azure.com/artem-horbunov/hor-cup/_build/latest?definitionId=1)
###Application for keeping statistic of played board games, optimized for mobile view.

## Technologies and libraries used:

- [NET 5.0](https://github.com/microsoft/dotnet)
- [Entity Framework Core 5.0](https://github.com/dotnet/efcore)
- [NUnit](https://github.com/nunit/nunit)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)
- [MediatR](https://github.com/jbogard/MediatR)
- [Automapper](https://github.com/AutoMapper/AutoMapper)
- [Angular 11](https://github.com/angular/angular)
- [Angular Material](https://github.com/angular/material)
- [Docker](https://www.docker.com/)

## Prerequisites:

- Microsoft SQL server
- Node 14.x
- Docker

## Run an application:

- navigate to src\HorCup.Presentation
- open command prompt and run command

```c#
dotnet run
```

- application will be available at https://localhost:5001

## Build a Docker image

- open `appsettings.Production.json` file and change `HorCupContext` value to connection string to ypour SQL database. Make sure TCP connection is enabled for your SQL server instance.
- navigate to `src\HorCup.Presentation`
- open cmd and run command

```dockerfile
docker build -t hor-cup-image .
```

- it will build a Docker image. To run it execute:

```dockerfile
docker run hor-cup-image --port=80
```

## To check unit tests coverage:

#### for BE:

- navigate to `tests\HorCup.Tests` and execute:

```c#
dotnet test --collect:"XPlat Code Coverage"
```

- it will generate folder `tests\HorCup.Tests\TestResults\{Guid}\coverage.cobertura.xml`
- make sure [report generator](https://github.com/danielpalme/ReportGenerator) is installed
- after run:

```c#
reportgenerator "-reports:{path to test result folder}\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
```

- report will e generated at `tests\HorCup.Tests\coveragereport`, open `index.html` file

### for FE:

- navigate to `src\HorCup.Presentation\ClientApp`
- open command prompt and run:

```js
npm run test:coverage
```
