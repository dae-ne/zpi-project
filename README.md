# ZPI Project

## Kanban board

When creating a new item in the Kanban board, add a prefix to the item/issue name indicating the team name. This will make it easier to filter tasks.

**Prefixes for tasks:**
- `[ui]` (UI/UX)
- `[front]` (frontend)
- `[back]` (backend)
- `[docs]` (documentation)
- `[pres]` (presentation/marketing)
- `[devops]` (DevOps)
- `[db]` (database)

Example: `[front] Create login page`

## Run with Docker Compose

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Email account with SMTP access (here's how to [generate a Gmail app password](https://support.google.com/accounts/answer/185833))

### Steps
1. Create `.env` file:
```bash
cp .env.template .env
```
2. Update `appsettings.Docker.json` in `server/src/Recipes.WebApi` with credentials:
```bash
cp server/src/Recipes.WebApi/appsettings.Docker.template.json server/src/Recipes.WebApi/appsettings.Docker.json
```
- `Email:Password` - e.g. Gmail app password. See - [generate a Gmail app password](https://support.google.com/accounts/answer/185833),
- `Email:AppEmail` - your email address.
3. Run docker-compose:
```bash
docker-compose up
```

### Ports

- `localhost:10000` - azurite storage emulator (blob storage)
- `localhost:8080` - ASP.NET Core Web API
- `localhost:5432` - PostgreSQL database
- frontend - not yet implemented

### Swagger

Swagger UI is available at: `localhost:8080/swagger` - [direct link](http://localhost:8080/swagger)

## License

This example is licensed under the [MIT License](LICENSE).
