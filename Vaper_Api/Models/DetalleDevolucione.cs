using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

[Table("Detalle_Devoluciones")]
public partial class DetalleDevolucione
{
    [Key]
    public int Id { get; set; }

    public int? DevolucionId { get; set; }

    public int? VentaPedidoId { get; set; }

    public int? Cantidad { get; set; }

    public int? UsuarioId { get; set; }

    [ForeignKey("DevolucionId")]
    [InverseProperty("DetalleDevoluciones")]
    public virtual Devolucione? Devolucion { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("DetalleDevoluciones")]
    public virtual Usuario? Usuario { get; set; }

    [ForeignKey("VentaPedidoId")]
    [InverseProperty("DetalleDevoluciones")]
    public virtual DetalleVentaPedido? VentaPedido { get; set; }
}
