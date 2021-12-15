using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Tasky.Models
{
    public partial class Cuota {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public bool Pagada { get; set; }
        public int PrestamoId { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal MontoRestante { get; set; }

        [NotMapped]
        public virtual int NumeroCuota {get; set;}
        public virtual Prestamo Prestamo { get; set; }
    }
}
