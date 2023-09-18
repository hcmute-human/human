default: run

run:
    dotnet run --project src/Human.Web

watch:
    dotnet watch --project src/Human.Web

build:
    dotnet build src/Human.Web

restore:
    dotnet restore

optimize:
    dotnet ef dbcontext optimize --project src/Human.Web -n Human.Infrastructure.Persistence.CompiledModels -o ../Human.Infrastructure/Persistence/CompiledModels

mig +arg:
    dotnet ef migrations {{arg}} --project src/Human.Web

db +arg:
    dotnet ef database {{arg}} --project src/Human.Web
