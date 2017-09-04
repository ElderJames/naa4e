Newer releases available here: https://naa4e.codeplex.com/
To have this sample working, you have to create the required database. To do that:
- web.config: update the "MerpReadModel" connection string and have it pointing to a running SQL Server instance
- execute the Update-Database command against the 'Merp.Accountancy.QueryStack' projects
- execute the Update-Database command against the 'Merp.Registry.QueryStack' projects