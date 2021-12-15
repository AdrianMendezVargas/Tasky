using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tasky.Data;
using Tasky.Models;

namespace Tasky.BLL {
    public class ClientesBLL {
        private readonly TaskyDbContext _db;

        public ClientesBLL(TaskyDbContext dbContext) {
            _db = dbContext;
        }

        public async Task<bool> Guardar(Cliente cliente) {
            if (await Existe(cliente)) {
                return await Actualizar(cliente);
            }

            return await Insertar(cliente);
        }

        public async Task<bool> Insertar(Cliente cliente) {
            bool guardado = false;

            try {
                await _db.Clientes.AddAsync(cliente);
                guardado = await _db.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }

            return guardado;
        }

        public async Task<bool> Actualizar(Cliente cliente) {
            bool guardado = false;

            try {
                 _db.Clientes.Update(cliente);
                guardado = await _db.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }

            return guardado;
        }

        public async Task<bool> Eliminar(Cliente cliente) {
            bool eliminado = false;

            if (!await Existe(cliente)) {
                return false;
            }

            try {
                _db.Clientes.Remove(cliente);
                eliminado = await _db.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }

            return eliminado;
        }

        public async Task<bool> Existe(Cliente cliente) {
            return await _db.Clientes.AnyAsync(c => c.Id == cliente.Id);  
        }

        public async Task<List<Cliente>> GetListAsync(Expression<Func<Cliente, bool>> expression) {

            var clientes = await _db.Clientes.Where(expression).ToListAsync();
            return clientes;
        }
    }
}
