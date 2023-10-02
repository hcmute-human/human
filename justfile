default: run

run:
    dotnet run --project src/Human.WebServer

watch:
    dotnet watch --project src/Human.WebServer

build:
    dotnet build src/Human.WebServer

restore:
    dotnet restore

optimize:
    dotnet ef dbcontext optimize --project src/Human.WebServer -n Human.Infrastructure.Persistence.CompiledModels -o ../Human.Infrastructure/Persistence/CompiledModels

mig +arg:
    dotnet ef migrations {{arg}} --project src/Human.WebServer

db +arg:
    dotnet ef database {{arg}} --project src/Human.WebServer
