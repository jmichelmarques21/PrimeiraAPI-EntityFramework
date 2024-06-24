using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.models;
using Microsoft.EntityFrameworkCore;

namespace Loja.data{
    public class LojaDbContext : DbContext{
        public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options){}

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venda> Vendas { get; set; }
    }
}