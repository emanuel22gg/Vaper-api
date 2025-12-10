using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class Imagene
{
    [Key]
    public int IdImagen { get; set; }

    [Column("URLImagen")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Urlimagen { get; set; }

    // Clave Foránea a Producto
    public int? ProductoId { get; set; }

    // Propiedad de Navegación de Referencia (Coincide con InverseProperty en Producto)
    [ForeignKey("ProductoId")]
    [InverseProperty("Imagenes")]
    public virtual Producto? Producto { get; set; }

    // *** ELIMINAR ESTO (La colección redundante que causa el error) ***
    // [InverseProperty("IdImagenNavigation")] 
    // public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}