CREATE USER [app] FROM LOGIN [app];
EXEC sp_addrolemember 'db_datareader', 'app';
EXEC sp_addrolemember 'db_datawriter', 'app';

CREATE USER [pipeline] FROM LOGIN [pipeline];
EXEC sp_addrolemember 'db_owner', 'pipeline';
