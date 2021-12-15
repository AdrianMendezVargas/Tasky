using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Tasky.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Prestamos = new HashSet<Prestamo>();
        }

        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public virtual ICollection<Prestamo> Prestamos { get; set; }
    }
}
