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

        // Método para gravar uma nova venda
        public async Task AddVendaAsync(Venda venda)
        {
            if (venda == null)
                throw new ArgumentNullException(nameof(venda));

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
    }
}
