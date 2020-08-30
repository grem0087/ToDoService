# ToDOService

## Migrations
### Migrations
1. To create go to ./ToDo.Database\
```
dotnet ef --startup-project ../ToDo.Service/ migrations add InitialMigration
```
InitialMigration - migration name

2. To apply:
```
dotnet ef --startup-project ../ToDo.Service/ database update
```

