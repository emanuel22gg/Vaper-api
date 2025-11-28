using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

[Table("Venta_Pedidos")]
public partial class VentaPedido
{
    [Key]
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? EstadoId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaEntrega { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? MetodoPago { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DireccionEntrega { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CiudadEntrega { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DepartamentoEntrega { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Subtotal { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Envio { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Total { get; set; }

    [InverseProperty("VentaPedido")]
    public virtual ICollection<Abono> Abonos { get; set; } = new List<Abono>();

    [InverseProperty("VentaPedido")]
    public virtual ICollection<DetalleVentaPedido> DetalleVentaPedidos { get; set; } = new List<DetalleVentaPedido>();

    [ForeignKey("EstadoId")]
    [InverseProperty("VentaPedidos")]
    public virtual Estado? Estado { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("VentaPedidos")]
    public virtual Usuario? Usuario { get; set; }
}
