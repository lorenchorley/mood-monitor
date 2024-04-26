Migration commands
===

The package Microsoft.EntityFrameworkCore.Tools is installed in the project MonitorDataAccess which is also where the DbContext is stored.
Open the Package Manager Console window and select this project to use the following commands.

Add new migration
---

To do after changing an aspect of an entity.

```
Add-Migration NameOfMigration
```

Remove an existing migration
---

```
Remove-Migration NameOfMigration
```

Apply migrations
---

To apply any necessary migrations to the current database

```
Update-Database
```

If you want to apply a particular migration

```
Update-Database NameOfMigration
```

If you want to remove all migration and return to a blank database

```
Update-Database 0
```


See also
---

Documentation : https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli