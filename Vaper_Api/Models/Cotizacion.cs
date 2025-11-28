using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vaper_Api.Models
{
    public partial class Cotizacion
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? NombreUsuario { get; set; } 

        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }

        [InverseProperty("Cotizacion")]
        public virtual ICollection<DetalleCotizacion> DetalleCotizaciones { get; set; } = new List<DetalleCotizacion>();
    }
}
