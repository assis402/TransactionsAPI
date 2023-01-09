using Microsoft.OpenApi.Models;
using MiniValidation;
using MongoDB.Driver;
using TransactionsAPI.Data;
using TransactionsAPI.DTOs;
using TransactionsAPI.Entity;
using static TransactionsAPI.Data.TransactionsDefinitions;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<TransactionsContextDb>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Transactions API",
        Description = "Developed by Matheus de Assis",
        Contact = new OpenApiContact { Name = "Matheus de Assis", Email = "assis4002@gmail.com" },
    });
});

var app = builder.Build();
#endregion

#region Configure Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

MapActions(app);
app.Run();

#endregion

#region Actions
void MapActions(WebApplication app)
{
    #region  Transactions
    app.MapPost("/transaction", async 
        (TransactionsContextDb database,
         TransactionCreateRequestDTO transactionDTO) => 
        {            
            if (!MiniValidator.TryValidate(transactionDTO, out var errors))
                return Results.ValidationProblem(errors);
            
            var transaction = new Transaction(transactionDTO);
            await database.Transactions.InsertOneAsync(transaction);
            return Results.Ok(transaction);
        })
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("CreateTransaction")
        .WithTags("Transaction");

    app.MapGet("/transaction", async
        (TransactionsContextDb database,
         string? period) => 
        {            
            if (period is null)
                return Results.BadRequest("The \"Period\" parameter is required.");

            var filter = GetByPeriodFilterDefinition(period);
            var result = await (await database.Transactions.FindAsync(filter)).ToListAsync();

            if(result.Count == 0)
                return Results.NoContent();

            return Results.Ok(result);
        })
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status204NoContent)
        .WithName("GetTransactions")
        .WithTags("Transaction");

    app.MapPut("/transaction", async
        (TransactionsContextDb database,
         TransactionUpdateRequestDTO transactionDTO) => 
        {            
            if (!MiniValidator.TryValidate(transactionDTO, out var errors))
                return Results.ValidationProblem(errors);

            var transaction = new Transaction(transactionDTO);

            var filter = GetByIdFilterDefinition(transactionDTO.Id!);   
            var updateDefinition = UpdateDefinition(transaction);
            var result = await database.Transactions.UpdateOneAsync(filter, updateDefinition);

            if(result.ModifiedCount == 0)
                Results.BadRequest("It was not possible to update the transaction with the given id.");    

            return Results.Ok(result);
        })
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("UpdateTransaction")
        .WithTags("Transaction");

    app.MapDelete("/transaction", async 
        (TransactionsContextDb database,
         string? id) => 
        {
            if (id is null)
                return Results.BadRequest("The \"Id\" parameter is required.");

            var filter = GetByIdFilterDefinition(id!);
            var result = await database.Transactions.DeleteOneAsync(filter); 

            if(result.DeletedCount == 0)
                Results.BadRequest("It was not possible to delete the transaction with the given id.");

            return Results.Ok(); 
        })
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("DeleteTransaction")
        .WithTags("Transaction");
}
#endregion