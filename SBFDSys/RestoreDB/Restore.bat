set PGPASSWORD=1234
"C:\Program Files\PostgreSQL\17\pgAdmin 4\runtime\psql.exe" -U postgres -p 5432 -h localhost -c "DROP DATABASE backupdata WITH (FORCE);"
"C:\Program Files\PostgreSQL\17\bin\createdb.exe" -h localhost -p 5432 -U postgres backupdata 
"C:\Program Files\PostgreSQL\17\pgAdmin 4\runtime\psql.exe" -U postgres -p 5432 -h localhost -d backupdata < "C:\\Users\\SKSI Dev\\Desktop\\SBFDSys\\SBFDSys\\Uploads\\newdata.dump"