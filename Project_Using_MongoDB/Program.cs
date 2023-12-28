using Project_Using_MongoDB;
using Project_Using_MongoDB.Interfaces;
using Project_Using_MongoDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBContext>(
     builder.Configuration.GetSection("MyDb")
    );

builder.Services.AddTransient<ICategoryRepos, CategoryRepos>();
builder.Services.AddTransient<IProductRepos, ProductRepos>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
