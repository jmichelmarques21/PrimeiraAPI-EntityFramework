using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.data;
using Loja.models;
using Microsoft.EntityFrameworkCore;

namespace Loja.services
{
    public class ClienteService
    {
        private readonly LojaDbContext _dbContext;

        public ClienteService(LojaDbContext dbContext) {
            _dbContext = dbContext;
        }

        //Método para consultar todos os clientes
        public async Task<List<Cliente>>GetAllClientesAsync(){
            return await _dbContext.Clientes.ToListAsync();
        }

        //Método para consultar um cliente a partir do seu id
        public async Task<Cliente>GetClienteByIdAsync(int id){
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Clientes.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        //Método para gravar um novo cliente
        public async Task AddClienteAsync(Cliente cliente){
            _dbContext.Clientes.Add(cliente);
            await _dbContext.SaveChangesAsync();
        }

        //Método para atualizar os dados de um cliente
        public async Task UpdateClienteAsync(Cliente cliente) {
            _dbContext.Entry(cliente).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        //Método para excluir um cliente
        public async Task DeleteClienteAsync(int id){
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente != null) {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}