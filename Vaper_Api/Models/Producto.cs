using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class Producto
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreProducto { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    // ✅ SOLO FK A CATEGORÍA
    public int? CategoriaId { get; set; }

    [Column(TypeName = "text")]
    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    // ✅ RELACIÓN CON CATEGORÍA
    [ForeignKey("CategoriaId")]
    [InverseProperty("Productos")]
    public virtual CategoriaProducto? Categoria { get; set; }

    // ✅ RELACIONES CON DETALLES
    [InverseProperty("Producto")]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [InverseProperty("Producto")]
    public virtual ICollection<DetalleVentaPedido> DetalleVentaPedidos { get; set; } = new List<DetalleVentaPedido>();

    [InverseProperty("Producto")]
    public virtual ICollection<DetalleCotizacion> DetalleCotizaciones { get; set; } = new List<DetalleCotizacion>();

    // ✅ ÚNICA RELACIÓN CON IMÁGENES (1 PRODUCTO → MUCHAS IMÁGENES)
    [InverseProperty("Producto")]
    public virtual ICollection<Imagene> Imagenes { get; set; } = new List<Imagene>();
    public int? IdImagen { get; internal set; }
}
