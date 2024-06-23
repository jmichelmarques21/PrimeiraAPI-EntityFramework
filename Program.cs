using Microsoft.AspNetCore.Mvc;
using Loja.models;
using Loja.data;
using Loja.services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<ClienteService>();
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

//Configurar as requisiçoes HTTP
if (app.Environment.IsDevelopment()){
  app.UseDeveloperExceptionPage();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

// endpoint criação de produto
app.MapPost("/createproduto", async (Produto produto, ProductService productService) => {
    await productService.AddProductAsync(produto);
    return Results.Created($"/createproduto/{produto.Id}", produto);
});

// endpoint consulta de produtos
app.MapGet("/produtos", async (ProductService productService) => {
  var produtos = await productService.GetAllProductsAsync();
  return Results.Ok(produtos);
});

// endpoint consulta de produto por id
app.MapGet("/produtos/{id}", async (int id, ProductService productService) => {
  var produto = await productService.GetProdutoByIdAsync(id);
  if (produto == null) {
    return Results.NotFound($"Produto with ID {id} not found.");
  }
  return Results.Ok(produto);
});

// endpoint atualização de produto por id
app.MapPut("/produtos/{id}", async (int id, Produto produto, ProductService productService) => {
    if (id != produto.Id){
      return Results.BadRequest("Product ID mismatch.");
    } 

    await productService.UpdateProductAsync(produto);
    return Results.Ok(produto);
  
  });

app.MapDelete("/produtos{id}", async (int id, ProductService productService) => {
  await productService.DeleteProductAsync(id);
  return Results.Ok();
});


// endpoint criação de cliente
app.MapPost("/createcliente", async (Cliente cliente, ClienteService clienteService) => {
  await clienteService.AddClienteAsync(cliente);
  return Results.Created($"/createcliente/{cliente.Id}", cliente);
});

// endpoint consulta de clientes
app.MapGet("/clientes", async (ClienteService clienteService) => {
  var clientes = await clienteService.GetAllClientesAsync();
  return Results.Ok(clientes);
});

// endpoint consulta de cliente por id
app.MapGet("/clientes/{id}", async (int id, ClienteService clienteService) => {
  var cliente = await clienteService.GetClienteByIdAsync(id);
  if (cliente == null) {
    return Results.NotFound($"Cliente with ID {id} not found.");
  }
  return Results.Ok(cliente);
});

// endpoint atualização do cliente pelo id
app.MapPut("/clientes/{id}", async (int id, Cliente cliente, ClienteService clienteService) => {
  if (id != cliente.Id) {
    return Results.BadRequest("Customer ID mismatch.");
  }

  await clienteService.UpdateClienteAsync(cliente);
  return Results.Ok(cliente);
});

// endpoint exclusão de cliente
app.MapDelete("/clientes{ìd}", async (int id, ClienteService clienteService) => {
  await clienteService.DeleteClienteAsync(id);
  return Results.Ok();
});

// endpoint criação de fornecedor
app.MapPost("/createfornecedor", async (Fornecedor fornecedor, FornecedorService fornecedorService) => {
  await fornecedorService.AddFornecedorAsync(fornecedor);
  return Results.Created($"createfornecedor/{fornecedor.Id}", fornecedor);
});

// endpoint consulta de fornecedores
app.MapGet("/fornecedores", async (FornecedorService fornecedorService) => {
  var fornecedores = await fornecedorService.GetAllFornecedoresAsync();
  return Results.Ok(fornecedores);
});

// endpoint consulta de fornecedor por id
app.MapGet("/fornecedores/{id}", async (int id, FornecedorService fornecedorService) => {
  var fornecedor = await fornecedorService.GetFornecedorByIdAsync(id);
  if (fornecedor == null) {
    return Results.NotFound($"Fornecedor with ID {id} not found.");
  }
  return Results.Ok(fornecedor);
});

// endpoint atualização de fornecedor por existingFornecedor
app.MapPut("/fornecedores/{id}", async (int id, Fornecedor fornecedor, FornecedorService fornecedorService) => {
  if (id != fornecedor.Id){
    return Results.BadRequest("Fornecedor ID mismatch");
  }
  await fornecedorService.UpdateFornecedorAsync(fornecedor);
  return Results.Ok(fornecedor);
} );

app.MapDelete("/fornecedores{id}", async (int id, FornecedorService fornecedorService) => {
  await fornecedorService.DeleteFornecedorAsync(id);
  return Results.Ok();
});

app.Run();