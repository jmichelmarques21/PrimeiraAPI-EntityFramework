using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.data;
using Loja.models;
using Microsoft.EntityFrameworkCore;

namespace Loja.services
{
    public class UsuarioService
    {
        private readonly LojaDbContext _dbContext;

        public UsuarioService(LojaDbContext dbContext) {
            _dbContext = dbContext;
        }

        //Método para consultar todos os fornecedores
        public async Task<List<Usuario>>GetAllUsuariosAsync(){
            return await _dbContext.Usuarios.ToListAsync();
        }

        //Método para consultar um fornecedor a partir do seu id
        public async Task<Usuario>GetUsuarioByIdAsync(int id){
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Usuarios.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        //Método para gravar um novo fornecedor
        public async Task AddUsuarioAsync(Usuario usuario){
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
        }

        //Método para atualizar os dados de um fornecedor
        public async Task UpdateUsuarioAsync(Usuario usuario) {
            _dbContext.Entry(usuario).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        //Método para excluir um fornecedor
        public async Task DeleteUsuarioAsync(int id){
            var usuario = await _dbContext.Usuarios.FindAsync(id);
            if (usuario != null) {
                _dbContext.Usuarios.Remove(usuario);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}