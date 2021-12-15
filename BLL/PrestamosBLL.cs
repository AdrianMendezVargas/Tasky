using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tasky.Data;
using Tasky.Models;

namespace Tasky.BLL {
    public class PrestamosBLL {

        private readonly TaskyDbContext _db;

        public PrestamosBLL(TaskyDbContext dbContext) {
            _db = dbContext;
        }

        public async Task<bool> Guardar(Prestamo prestamo) {
            if (await Existe(prestamo)) {
                return await Actualizar(prestamo);
            }

            return await Insertar(prestamo);
        }

        public async Task<bool> Insertar(Prestamo prestamo) {
            bool guardado = false;

            try {
                await _db.Prestamos.AddAsync(prestamo);
                guardado = await _db.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }

            return guardado;
        }

        public async Task<bool> Actualizar(Prestamo prestamo) {
            bool guardado = false;

            try {
                _db.Prestamos.Update(prestamo);
                guardado = await _db.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }

            return guardado;
        }

        public async Task<bool> Eliminar(Prestamo prestamo) {
            bool eliminado = false;

            if (!await Existe(prestamo)) {
                return false;
            }

            try {
                _db.Prestamos.Remove(prestamo);
                eliminado = await _db.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }

            return eliminado;
        }

        public async Task<bool> Existe(Prestamo prestamo) {
            return await _db.Prestamos.AnyAsync(c => c.Id == prestamo.Id);
        }

        public async Task<List<Prestamo>> GetListAsync(Expression<Func<Prestamo , bool>> expression) {

            var clientes = await _db.Prestamos.Where(expression).Include(p=>p.Cuotas).ToListAsync();
            return clientes;
        }

    }
}
