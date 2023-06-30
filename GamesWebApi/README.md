## Commands

### Run tests
1. `docker compose up -d`
2. Create database in container `CREATE DATABASE GamesDB;`. You can use DBeaver.
3. `cd ../GamesWebApi && dotnet test --collect:"Xplat Code Coverage"`

### Build project

- `dotnet restore`

### Run application
1. `docker compose up -d`
2. Create database in container `CREATE DATABASE GamesDB;`. You can use DBeaver.
3. `cd ../GamesWebApi/GamesWebApi.Presentation && dotnet run`
4. Access [http://localhost:5002/swagger/index.html](http://localhost:5002/swagger/index.html).
