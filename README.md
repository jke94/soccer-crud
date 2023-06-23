# .NET Web API demo: A CRUD Application

## soccer-crud solution.

.NET Web Api: CRUD application (Create, Read, Update and Delete) basic operations. Focus on how to implement different technologies.

Current state of development:

| Status | Description | 
| :---: |     :---    |
| ✅ | Database persistence (using EF Core as ORM). |
| ❔ | Docker-compose to create differnt containers for database and webapi |
| ❔ | Web Api Authentication |
| ❔ | Web Api Authorization |
| ❔ | Web Api Health checks |
| ✅ | Solution with architecture layers (services with application logic, repository pattern, data access logic and data seeded for development environment) |
| ❔ | FastEndpoints (pending) |

Legend (status implementation):
- ✅ Implemented
- ❔ Pending


## Docker-compose: Start SoccerCrud.WebApi.

- Windows:

```
docker-compose.exe -f ./docker-compose.yml -p soccer-crud up --build
```

- Linux
```
sudo docker compose -f ./docker-compose.yml -p soccer-crud up --build
```