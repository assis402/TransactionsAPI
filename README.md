# TransactionsAPI
Minimal API with C# .NET 7 and Data Persistence in MongoDB. 

This API was built to be consumed by the App "continhas" (). Its function is to perform inclusion, update, deletion and queries of financial inputs and outputs executed in the App.

## Structure
```
└── Solution
    ├── API                   // Presentation and business layer (Minimal API)
    │   ├── Builders          // Logic for building complex objects step by step
    │   ├── Convertes         // Mapping entities to Dto
    │   ├── Data              // MongoDB database configurations
    │   ├── DTOs              // Data transfer objects to API response 
    │   ├── Entities          // Entities related to Transactions with their respective business logic
    │   ├── Helpers           // Utility classes with specific goals
    │   ├── appsettings.json  // Contains environment variables, database connection string, log configuration...
    │   └── Program.cs        // Application startup and configuration, dependency injections and end-point mapping
    │    
    └── IntegrationTests                // Integration Tests layer 
        ├── Helpers                     // Utility classes to build Dtos for testing
        ├── TransactionsApplication.cs  // In-memory API initialization
        └── TransactionTests.cs         // Testing in the context of Transactions
```

## API

#### /transaction
* `POST` : Create a new transaction
* `PUT` : Update a transaction

#### /transaction/:id
* `DELETE` : Delete a transaction by id

#### /transaction/:period
* `GET` : Get transactions by period
* `DELETE` : Delete transactions by period

## To Do

- [x] Transactions CRUD.
- [x] Basic Swagger.
- [ ] Support Authentication with user for securing the APIs.
- [ ] Write integration tests for Dashboard endpoints.
- [ ] Deploy at jenkins 
