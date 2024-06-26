using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.data;
using Loja.models;
using Microsoft.EntityFrameworkCore;

namespace Loja.services
{
    public class VendaService
    {
        private readonly LojaDbContext _dbContext;

        public VendaService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para consultar todas as vendas
        public async Task<List<Venda>> GetAllVendasAsync()
        {
            return await _dbContext.Vendas.AsNoTracking().ToListAsync();
        }

        // Método para consultar uma venda a partir da sua NF
        public async Task<Venda> GetVendaByIdAsync(int id)
        {
            return await _dbContext.Vendas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
        }

        // Método para gravar uma nova venda com validação de cliente e produto
        public async Task AddVendaAsync(Venda venda, ClienteService clienteService, ProductService productService)
        {
            if (venda == null)
                throw new ArgumentNullException(nameof(venda));

            // Validar se o cliente existe
            var clienteExiste = await clienteService.GetClienteByIdAsync(venda.Cliente.Id);
            if (clienteExiste == null)
                throw new ArgumentException("Cliente não encontrado.");

            // Validar se o produto existe
            var produtoExiste = await productService.GetProdutoByIdAsync(venda.Produto.Id);
            if (produtoExiste == null)
                throw new ArgumentException("Produto não encontrado.");

            _dbContext.Vendas.Add(venda);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de uma venda
        public async Task UpdateVendaAsync(Venda venda)
        {
            if (venda == null)
                throw new ArgumentNullException(nameof(venda));

            _dbContext.Entry(venda).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para excluir uma venda
        public async Task DeleteVendaAsync(int id)
        {
            var venda = await _dbContext.Vendas.FindAsync(id);
            if (venda != null)
            {
                _dbContext.Vendas.Remove(venda);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Método para consultar vendas detalhadas por produto
        public async Task<List<VendaDetalhadaDto>> GetVendasDetalhadasPorProdutoAsync(int produtoId)
        {
            return await _dbContext.Vendas
                .Where(v => v.Produto.Id == produtoId)
                .Select(v => new VendaDetalhadaDto
                {
                    ProdutoNome = v.Produto.Nome,
                    DataVenda = v.DataVenda,
                    VendaId = v.Id,
                    ClienteNome = v.Cliente.Nome,
                    Quantidade = v.Quantidade,
                    Preco = v.Preco
                })
                .ToListAsync();
        }
    }

    // DTO para retornar os dados detalhados da venda
    public class VendaDetalhadaDto
    {
        public string ProdutoNome { get; set; }
        public string DataVenda { get; set; }
        public int VendaId { get; set; }
        public string ClienteNome { get; set; }
        public int Quantidade { get; set; }
        public float Preco { get; set; }
    }
}