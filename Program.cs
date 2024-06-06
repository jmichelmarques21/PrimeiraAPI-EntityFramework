using Microsoft.EntityFrameworkCore;
using Loja.data;
using Loja.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar a conexão com o BD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options=>options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 2))));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/createproduto", async (LojaDbContext dbContext, Produto newProduto) => {
  dbContext.Produtos.Add(newProduto);
  await dbContext.SaveChangesAsync();
  return Results.Created($"/createproduto/{newProduto.Id}", newProduto);
});