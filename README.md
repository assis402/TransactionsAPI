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
   └── IntegrationTests            // Integration Tests layer 
       ├── Helpers                     // Utility classes to build Dtos for testing
       ├── TransactionsApplication.cs  // In-memory API initialization
       └── TransactionTests.cs         // Testing in the context of Transactions
```

## API

#### /projects
* `GET` : Get all projects
* `POST` : Create a new project

#### /projects/:title
* `GET` : Get a project
* `PUT` : Update a project
* `DELETE` : Delete a project

#### /projects/:title/archive
* `PUT` : Archive a project
* `DELETE` : Restore a project 

#### /projects/:title/tasks
* `GET` : Get all tasks of a project
* `POST` : Create a new task in a project

#### /projects/:title/tasks/:id
* `GET` : Get a task of a project
* `PUT` : Update a task of a project
* `DELETE` : Delete a task of a project

#### /projects/:title/tasks/:id/complete
* `PUT` : Complete a task of a project
* `DELETE` : Undo a task of a project

## Todo

- [x] Support basic REST APIs.
- [ ] Support Authentication with user for securing the APIs.
- [ ] Make convenient wrappers for creating API handlers.
- [ ] Write the tests for all APIs.
- [x] Organize the code with packages
- [ ] Make docs with GoDoc
- [ ] Building a deployment process 
