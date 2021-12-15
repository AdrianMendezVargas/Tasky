using System;
using System.Collections.Generic;

#nullable disable

namespace Tasky.Models
{
    public partial class Pago
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int PrestamoId { get; set; }

        public virtual Prestamo Prestamo { get; set; }
    }
}
