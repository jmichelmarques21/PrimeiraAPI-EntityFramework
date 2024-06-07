using Microsoft.EntityFrameworkCore;
using Loja.data;
using Loja.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configurar a conexão com o BD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 2))));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

// endpoint criação de produto
app.MapPost("/createproduto", async (LojaDbContext dbContext, Produto newProduto) => {
    dbContext.Produtos.Add(newProduto);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/createproduto/{newProduto.Id}", newProduto);
});

// endpoint consulta de produtos
app.MapGet("/produtos", async (LojaDbContext dbContext) => {
  var produtos = await dbContext.Produtos.ToListAsync();
  return Results.Ok(produtos);
});

// endpoint consulta de produto por id
app.MapGet("/produtos/{id}", async (int id, LojaDbContext dbContext) => {
  var produto = await dbContext.Produtos.FindAsync(id);
  if (produto == null) {
    return Results.NotFound($"Produto with ID {id} not found.");
  }
  return Results.Ok(produto);
});

// endpoint atualização de produto por id
app.MapPut("/produtos/{id}", async (int id, LojaDbContext dbContext, Produto updateProduto) => {
  var existingProduto = await dbContext.Produtos.FindAsync(id);
  if (existingProduto == null) {
    return Results.NotFound($"Produto with ID {id} not found.");
  }

  // atualizar os dados do produto existente
  existingProduto.Nome = updateProduto.Nome;
  existingProduto.Preco = updateProduto.Preco;
  existingProduto.Fornecedor = updateProduto.Fornecedor;

  // salvando no banco de dados
  await dbContext.SaveChangesAsync();

  // retorno do endpoint
  return Results.Ok(existingProduto);
});


// endpoint criação de cliente
app.MapPost("/createcliente", async (LojaDbContext dbContext, Cliente newCliente) => {
  dbContext.Clientes.Add(newCliente);
  await dbContext.SaveChangesAsync();
  return Results.Created($"createcliente/{newCliente.Id}", newCliente);
});

// endpoint consulta de clientes
app.MapGet("/clientes", async (LojaDbContext dbContext) => {
  var clientes = await dbContext.Clientes.ToListAsync();
  return Results.Ok(clientes);
});

// endpoint consulta de cliente por id
app.MapGet("/clientes/{id}", async (int id, LojaDbContext dbContext) => {
  var cliente = await dbContext.Clientes.FindAsync(id);
  if (cliente == null) {
    return Results.NotFound($"Cliente with ID {id} not found.");
  }
  return Results.Ok(cliente);
});

// endpoint atualização do cliente pelo id
app.MapPut("/clientes/{id}", async (int id, LojaDbContext dbContext, Cliente updateCliente) => {
  var existingCliente = await dbContext.Clientes.FindAsync(id);
  if (existingCliente == null) {
    return Results.NotFound($"Produto with ID {id} not found.");
  }

  // atualizar os dados do produto existente
  existingCliente.Nome = updateCliente.Nome;
  existingCliente.Cpf = updateCliente.Cpf;
  existingCliente.Email = updateCliente.Email;

  // salvando no banco de dados
  await dbContext.SaveChangesAsync();

  // retorno do endpoint
  return Results.Ok(existingCliente);
});








app.Run();