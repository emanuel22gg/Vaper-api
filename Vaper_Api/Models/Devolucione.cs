using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class Devolucione
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [Column(TypeName = "text")]
    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    [InverseProperty("Devolucion")]
    public virtual ICollection<DetalleDevolucione> DetalleDevoluciones { get; set; } = new List<DetalleDevolucione>();
}
