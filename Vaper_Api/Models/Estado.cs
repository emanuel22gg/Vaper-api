using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class Estado
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? NombreEstado { get; set; }

    [InverseProperty("EstadoNavigation")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    [InverseProperty("Estado")]
    public virtual ICollection<VentaPedido> VentaPedidos { get; set; } = new List<VentaPedido>();
}
