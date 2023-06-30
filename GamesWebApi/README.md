## Commands

### Run tests
1. `cd ../ImagesServer && docker compose up -d`
2. `cd ../GamesWebApi && dotnet test --collect:"Xplat Code Coverage"`

### Build project

- `dotnet restore`

### Run application
1. `cd ../ImagesServer && docker compose up -d`
2. `cd ../GamesWebApi/GamesWebApi.Presentation && dotnet run`
3. Acessar [http://localhost:5002/swagger/index.html](http://localhost:5002/swagger/index.html)
