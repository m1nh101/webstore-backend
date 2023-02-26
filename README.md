# All Things You Need

## Template appsettings.json

``` json
{
  "Logging":  {
   "LogLevel":  {
    "Default":  "Information",
    "Microsoft.AspNetCore":  "Warning"
   }
  },
  "ConnectionStrings":  {
   "WebStore":  ""
  },
 "AllowedHosts":  "*"
 }
```

## database migrations

``` bash
    #dotnet cli (path is inside src folder)
    dotnet ef update database --project WebStore.Infrastructure --startup-project WebStore.API
    
    #intergrated vs terminal (WebStore.API as startup project, WebStore.Infrastructure as default)
    Update-Database
```
