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

    public int? CategoriaId { get; set; }

    public int? IdImagen { get; set; }

    [Column("URLImagen")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Urlimagen { get; set; }

    public bool? Estado { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("Productos")]
    public virtual CategoriaProducto? Categoria { get; set; }

    [InverseProperty("Producto")]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [InverseProperty("Producto")]
    public virtual ICollection<DetalleVentaPedido> DetalleVentaPedidos { get; set; } = new List<DetalleVentaPedido>();

    [ForeignKey("IdImagen")]
    [InverseProperty("Productos")]
    public virtual Imagene? IdImagenNavigation { get; set; }

    [InverseProperty("Producto")]
    public virtual ICollection<Imagene> Imagenes { get; set; } = new List<Imagene>();
}
