using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class Usuario
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Nombres { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Apellidos { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Correo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Contraseña { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? TipoDocumento { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? NumeroDocumento { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Ciudad { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? Barrio { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public bool? EstadoUsuario { get; set; }

    public int? RolId { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<DetalleDevolucione> DetalleDevoluciones { get; set; } = new List<DetalleDevolucione>();

    [ForeignKey("RolId")]
    [InverseProperty("Usuarios")]
    public virtual Role? Rol { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<VentaPedido> VentaPedidos { get; set; } = new List<VentaPedido>();
}
