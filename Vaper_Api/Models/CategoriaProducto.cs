using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

[Table("Categoria_Productos")]
public partial class CategoriaProducto
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreCategoria { get; set; }

    [Column(TypeName = "text")]
    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    [InverseProperty("Categoria")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}


