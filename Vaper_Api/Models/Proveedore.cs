using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class Proveedore
{
    [Key]
    public int Id { get; set; }

    [Column("NombreCompleto_o_RazonSocial")]
    [StringLength(100)]
    [Unicode(false)]
    public string? NombreCompletoORazonSocial { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? TipoDocumento { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? NumeroDocumento { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? RepresentanteLegal { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [Column(TypeName = "text")]
    public string? Direccion { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Ciudad { get; set; }

    public bool Estado { get; set; }

    [InverseProperty("Proveedor")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
