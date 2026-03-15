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

    public int? DetalleVentaPedidoId { get; set; }

    public int? Cantidad { get; set; }


    [ForeignKey("DevolucionId")]
    [InverseProperty("DetalleDevoluciones")]
    public virtual Devolucione? Devolucion { get; set; }

    [ForeignKey("DetalleVentaPedidoId")]
    [InverseProperty("DetalleDevoluciones")]
    public virtual DetalleVentaPedido? DetalleVentaPedido { get; set; }
}
