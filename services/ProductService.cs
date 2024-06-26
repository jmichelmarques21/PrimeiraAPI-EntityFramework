using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.data;
using Loja.models;
using Microsoft.EntityFrameworkCore;

namespace Loja.services
{
    public class ProductService
    {
        private readonly LojaDbContext _dbContext;

        public ProductService(LojaDbContext dbContext) {
            _dbContext = dbContext;
        }

        //Método para consultar todos os produtos
        public async Task<List<Produto>>GetAllProductsAsync(){
            return await _dbContext.Produtos.ToListAsync();
        }

        //Método para consultar um produto a partir do seu id
        public async Task<Produto>GetProdutoByIdAsync(int id){
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Produtos.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        //Método para gravar um novo produto
        public async Task AddProductAsync(Produto produto){
            _dbContext.Produtos.Add(produto);
            await _dbContext.SaveChangesAsync();
        }

        //Método para atualizar os dados de um produto
        public async Task UpdateProductAsync(Produto produto) {
            _dbContext.Entry(produto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        //Método para excluir um produto
        public async Task DeleteProductAsync(int id){
            var produto = await _dbContext.Produtos.FindAsync(id);
            if (produto != null) {
                _dbContext.Produtos.Remove(produto);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}