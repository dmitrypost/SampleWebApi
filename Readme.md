[Project setup]
Create and update the `.env` file with the values found in the `.env.example` file.

[Database setup]
Use entity framework to create the database and tables. Run the following commands in the terminal:
`dotnet ef database update --project ./SampleWebApi.Infrastructure/SampleWebApi.Infrastructure.csproj`

For entity framework commands, you may need to install the ef tool globally. Run the following command in the terminal:
`dotnet tool install --global dotnet-ef`

To push a new migration, run the following command in the terminal:
`dotnet ef migrations add <MigrationName> --project ./SampleWebApi.Infrastructure/SampleWebApi.Infrastructure.csproj`

Error handling:
`ErrorHandlingMiddleware.cs` is used to handle exceptions globally. It catches all exceptions and returns a response with the status code 500 and the error message.
`LoggingPipeline.cs` is used to log the request input and function name to the logger.
`Logger.cs` is used to log the error message to the implemented logger.
`NLog.config` is used to configure the logger. It reads the environment variable for the database connection string for logging to the database.