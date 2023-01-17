using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using MiniValidation;
using MongoDB.Driver;
using Transactions.API.Data;
using Transactions.API.DTOs.Request;
using Transactions.API.DTOs.Response;
using Transactions.API.Entities;
using static Microsoft.AspNetCore.Http.Results;
using static Transactions.API.Data.TransactionsDefinitions;
using static Transactions.API.Converters.TransactionConverters;
using static Transactions.API.Helpers.ResultHelper;
using System.Net;
using Transactions.API.Helpers;
using Transactions.API.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");

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
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

MapTransactionActions(app);
app.Run();
#endregion

#region Transaction Actions
static void MapTransactionActions(WebApplication app)
{
    app.MapPost("/transaction", async 
        (TransactionsContextDb database,
         TransactionCreateRequestDTO transactionDTO) => 
        {
            try
            {
                if (!MiniValidator.TryValidate(transactionDTO, out var errors))
                    return BadRequest(ErrorResult(errors));
                
                var transaction = new Transaction(transactionDTO);
                await database.Transactions.InsertOneAsync(transaction);

                return SuccessResult<TransactionResponseDTO>(transaction);
            }
            catch (Exception ex)
            {
                return CriticalErrorResult(ex.Message);
            }
        })
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
                return NotFoundResult("No transactions found.");

            return SuccessResult(result.ConvertToDTO());
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
                ErrorResult("It was not possible to update the transaction with the given id.");    

            return SuccessResult<TransactionResponseDTO>(transaction);
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
                ErrorResult("It was not possible to delete the transaction with the given id.");

            return SuccessResult($"Transaction with id {id} successfully deleted"); 
        })
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("DeleteTransaction")
        .WithTags("Transaction");
}
#endregion

#region  FrequentTransaction Actions

#endregion

public partial class Program {}