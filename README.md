# Get Started

## Para levantar el Proyecto

### primero

crear el contener de la base de datos:

```yaml
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Passw0rd123@" -p 1433:1433 -d --network bridge --name sqldb mcr.microsoft.com/mssql/server
```

### Segundo
levantar el docker compose de la aplicacion:


## Que contiene el proyecto

* Aplicacion en net core 6
* Base de datos en SQL
* recolector de errores con ELK
* Observabilidad con Jaeger.

## Composicion del repositorio