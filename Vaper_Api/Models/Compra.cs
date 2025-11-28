using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

[Index("NumeroCompra", Name = "UQ__Compras__5F9B8DECF727A666", IsUnique = true)]
public partial class Compra
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NumeroCompra { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaCompra { get; set; }

    public int? ProveedorId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Subtotal { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Total { get; set; }

    public int? Estado { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [InverseProperty("Compra")]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [ForeignKey("Estado")]
    [InverseProperty("Compras")]
    public virtual Estado? EstadoNavigation { get; set; }

    [ForeignKey("ProveedorId")]
    [InverseProperty("Compras")]
    public virtual Proveedore? Proveedor { get; set; }
}
