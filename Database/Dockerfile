FROM mcr.microsoft.com/mssql/server:2019-latest

ENV MSSQL_SA_PASSWORD='1Secure*Password1'
ENV ACCEPT_EULA=Y

COPY ./backup.bak /var/opt/mssql/backup/backup.bak

CMD /opt/mssql-tools/bin/sqlcmd -U 'sa' -P '1Secure*Password1' -Q 'RESTORE DATABASE PharmacyManagerDB FROM DISK="/var/opt/mssql/backup/backup.bak" WITH MOVE "PharmacyManagerDB" TO "/var/opt/mssql/data/PharmacyManagerDB.mdf", MOVE "PharmacyManagerDB_log" TO "/var/opt/mssql/data/PharmacyManagerDB_log.ldf";' & /opt/mssql/bin/sqlservr
