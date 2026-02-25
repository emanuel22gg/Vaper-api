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

    // Añadidos: campos solicitados
    [StringLength(50)]
    [Unicode(false)]
    public string? Codigo { get; set; }

    [Column("NombreCompleto_o_RazonSocial")]
    [StringLength(100)]
    [Unicode(false)]
    public string? NombreCompletoORazonSocial { get; set; }

    // Tipo de persona: natural | juridica
    [StringLength(20)]
    [Unicode(false)]
    public string? TipoPersona { get; set; }

    // Campos para persona natural
    [StringLength(100)]
    [Unicode(false)]
    public string? Nombres { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Apellidos { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Cedula { get; set; }

    // Campos para persona juridica
    [StringLength(100)]
    [Unicode(false)]
    public string? RazonSocial { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Nit { get; set; }

    // Contactos
    [StringLength(20)]
    [Unicode(false)]
    public string? Celular { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Pais { get; set; }

    // Lista de productos como JSON en texto
    [Column(TypeName = "text")]
    public string? Productos { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UltimaCompra { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalCompras { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Banco { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? NumeroCuenta { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? TipoCuenta { get; set; }

    // Contacto adicional (nombre, cargo, telefono, email) como JSON
    [Column(TypeName = "text")]
    public string? ContactoAdicional { get; set; }

    [Column(TypeName = "text")]
    public string? Observaciones { get; set; }

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

    // Nueva propiedad: Método de pago preferido
    [StringLength(50)]
    [Unicode(false)]
    public string? MetodoPagoPreferido { get; set; }

    // Nuevas propiedades para geolocalización
    public double? Latitud { get; set; }
    public double? Longitud { get; set; }

    // Nuevo campo para información adicional en formato JSON
    [Column(TypeName = "text")]
    public string? InformacionAdicional { get; set; }

    public bool Estado { get; set; }

    [InverseProperty("Proveedor")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
