using Fina.Api.Data;
using Fina.Api.Services;
using Fina.Common.Requests.Categories;
using Fina.Common.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = "Server=172.17.0.2;User Id=Fina;Password=1234;Database=fina";

builder.Services.AddDbContext<AppDbContext>(x => x.UseMySql(connectionString, 
    ServerVersion.AutoDetect(connectionString)));

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

var app = builder.Build();


app.Run();
