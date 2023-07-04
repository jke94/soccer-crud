# .NET Web API demo: A CRUD Application

.NET Web Api: CRUD application (Create, Read, Update and Delete) basic operations. Focus on how to implement different technologies.

## A. Development status.

Legend (status implementation):
- ✅ Implemented
- ❔ Pending

Current state of development:

|| Status | Description | 
| :---: | :---: |     :---    |
|1| ✅ | Database persistence (using EF Core as ORM). |
|2| ✅ | Docker-compose to create differnt containers for database and webapi |
|3| ❔ | Web Api Authentication |
|4| ❔ | Web Api Authorization |
|5| ❔ | Web Api Health checks |
|6| ✅ | Solution with architecture layers (services with application logic, repository pattern, data access logic and data seeded for development environment) |
|7| ❔ | FastEndpoints (pending) |
|8| ❔ | Integration tests |

## B. Start SoccerCrud.WebApi (docker-compose)

- Windows:

```
docker-compose.exe -f ./docker-compose.yml -p soccer-crud up --build
```

- Linux
```
sudo docker compose -f ./docker-compose.yml -p soccer-crud up --build
```

After running the last command open the URL:

- http://localhost:5112/swagger/index.html

## C. Useful information.

- [Supercharging ASP.NET 6.0 with ODATA | CRUD | Batching | Pagination](https://dev.to/renukapatil/supercharging-aspnet-60-with-odata-crud-batching-pagination-12np)
- [How to Compose an ASP.NET Core Web API (.NET 6) with an MS SQL Server 2022 on Linux in Docker](https://blog.christian-schou.dk/dockerize-net-core-web-api-with-ms-sql-server/)