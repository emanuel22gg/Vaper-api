using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

[Table("Detalle_Compras")]
public partial class DetalleCompra
{
    [Key]
    public int Id { get; set; }

    public int? CompraId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PrecioUnitario { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Subtotal { get; set; }

    [ForeignKey("CompraId")]
    [InverseProperty("DetalleCompras")]
    public virtual Compra? Compra { get; set; }

    [ForeignKey("ProductoId")]
    [InverseProperty("DetalleCompras")]
    public virtual Producto? Producto { get; set; }
}
