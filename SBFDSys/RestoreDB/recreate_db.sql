DROP DATABASE backupdata;

CREATE DATABASE backupdata
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'English_Indonesia.1252'
       LC_CTYPE = 'English_Indonesia.1252'
       CONNECTION LIMIT = -1;