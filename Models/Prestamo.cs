using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Tasky.Models
{
    public partial class Prestamo
    {
        public Prestamo()
        {
            Cuotas = new List<Cuota>();
            Pagos = new List<Pago>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public double InteresAnual { get; set; }
        [Required]
        public int PlazoMeses { get; set; }
        [Required]
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public virtual Cliente Cliente { get; set; }
        public virtual List<Cuota> Cuotas { get; set; }
        public virtual List<Pago> Pagos { get; set; }
    }
}
