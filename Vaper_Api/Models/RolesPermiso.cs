using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

[Table("Roles_Permisos")]
public partial class RolesPermiso
{
    [Key]
    public int Id { get; set; }

    public int? RolId { get; set; }

    public int? PermisoId { get; set; }

    [ForeignKey("PermisoId")]
    [InverseProperty("RolesPermisos")]
    public virtual Permiso? Permiso { get; set; }

    [ForeignKey("RolId")]
    [InverseProperty("RolesPermisos")]
    public virtual Role? Rol { get; set; }
}
