using CashFlow.Api.Filters;
using CashFlow.Application;
using CashFlow.Application.Middleware;
using CashFlow.Infrastructure;
using CashFlow.Infrastucture.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
var app = builder.Build();

app.UseMiddleware<CultureMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await MigrateDataBase();

app.Run();

async Task MigrateDataBase() 
{
    await using var scope = app.Services.CreateAsyncScope();
    await DataBaseMigration.MigrateDataBase(scope.ServiceProvider);
}
