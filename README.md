primero ejecutar.

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Passw0rd123@" -p 1433:1433 -d --network bridge --name sqldb mcr.microsoft.com/mssql/server