using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Transactions.Business.Interfaces.Repositories;
using Transactions.Business.Interfaces.Services;
using Transactions.Business.Services;
using Transactions.Infrastructure;
using Transactions.Infrastructure.Helpers;
using Transactions.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

if (builder.Environment.IsDevelopment())
    DotEnvLoader.Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<TransactionsContextDb>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Transactions API",
        Description = "Developed by Matheus de Assis and Davi Arruda",
        Contact = new OpenApiContact { Name = "Matheus de Assis", Email = "assis4002@gmail.com" },
    });
});
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

#endregion Configure Services

#region Configure Pipeline

if (app.Environment.IsProduction())
{
    app.UseForwardedHeaders();
    app.UseSwagger(options =>
    {
        options.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            if (httpReq.Headers.ContainsKey("Host"))
            {
                var basePath = "matheus/transactions-api";
                var serverUrl = $"{httpReq.Scheme}://{httpReq.Headers["Host"]}/{basePath}";
                swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = serverUrl } };
            }
        });
    })
    .UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
        options.SwaggerEndpoint("v1/swagger.json", "Transactions API (v1)");
    });
}
else
{
    app.UseSwagger().UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
        options.SwaggerEndpoint("v1/swagger.json", "Transactions API (v1)");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

#endregion Configure Pipeline

public partial class Program
{ }