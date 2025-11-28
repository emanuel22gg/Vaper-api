using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

[Table("Detalle_Venta_Pedidos")]
public partial class DetalleVentaPedido
{
    [Key]
    public int Id { get; set; }

    public int? VentaPedidoId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PrecioUnitario { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Subtotal { get; set; }

    [InverseProperty("VentaPedido")]
    public virtual ICollection<DetalleDevolucione> DetalleDevoluciones { get; set; } = new List<DetalleDevolucione>();

    [ForeignKey("ProductoId")]
    [InverseProperty("DetalleVentaPedidos")]
    public virtual Producto? Producto { get; set; }

    [ForeignKey("VentaPedidoId")]
    [InverseProperty("DetalleVentaPedidos")]
    public virtual VentaPedido? VentaPedido { get; set; }
}
