using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class Devolucione
{
    [Key]
    public int Id { get; set; }

    [Column("FechaDevolucion", TypeName = "datetime")]
    public DateTime? FechaDevolucion { get; set; }

    [Column(TypeName = "text")]
    public string? Descripcion { get; set; }

    public int? VentaPedidoId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? MontoTotal { get; set; }

    public int? EstadoId { get; set; }

    [ForeignKey("VentaPedidoId")]
    [InverseProperty("Devoluciones")]
    public virtual VentaPedido? VentaPedido { get; set; }

    [ForeignKey("EstadoId")]
    [InverseProperty("Devoluciones")]
    public virtual Estado? EstadoNavigation { get; set; }

    [InverseProperty("Devolucion")]
    public virtual ICollection<DetalleDevolucione> DetalleDevoluciones { get; set; } = new List<DetalleDevolucione>();
}
