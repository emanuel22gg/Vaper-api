using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class Abono
{
    [Key]
    public int Id { get; set; }

    public int? VentaPedidoId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Monto { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? SaldoRestante { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? MetodoPago { get; set; }

    public bool? Estado { get; set; }

    [ForeignKey("VentaPedidoId")]
    [InverseProperty("Abonos")]
    public virtual VentaPedido? VentaPedido { get; set; }
}
