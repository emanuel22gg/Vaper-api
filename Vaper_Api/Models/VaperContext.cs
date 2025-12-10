using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Vaper_Api.Models;

public partial class VaperContext : DbContext
{
    public VaperContext()
    {
    }

    public VaperContext(DbContextOptions<VaperContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abono> Abonos { get; set; }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Cotizacion> Cotizaciones { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleCotizacion> DetalleCotizaciones { get; set; }

    public virtual DbSet<DetalleDevolucione> DetalleDevoluciones { get; set; }

    public virtual DbSet<DetalleVentaPedido> DetalleVentaPedidos { get; set; }

    public virtual DbSet<Devolucione> Devoluciones { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Imagene> Imagenes { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VentaPedido> VentaPedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VaperOneDb.mssql.somee.com;Database=VaperOneDb;User Id=DiegoVelasquez_SQLLogin_1;Password=89doreb7bj;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abono>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Abonos__3214EC078D852302");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.VentaPedido).WithMany(p => p.Abonos).HasConstraintName("FK__Abonos__VentaPed__0D7A0286");
        });

        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC079C3563B3");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compras__3214EC07838F141B");

            entity.Property(e => e.FechaCompra).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Compras).HasConstraintName("FK__Compras__Estado__17036CC0");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Compras).HasConstraintName("FK__Compras__Proveed__160F4887");
        });

        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cotizaci__3214EC07");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalle___3214EC070BE39BA9");

            entity.HasOne(d => d.Compra).WithMany(p => p.DetalleCompras).HasConstraintName("FK__Detalle_C__Compr__19DFD96B");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleCompras).HasConstraintName("FK__Detalle_C__Produ__1AD3FDA4");
        });

        modelBuilder.Entity<DetalleCotizacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalle___3214EC07");

            entity.HasOne(d => d.Cotizacion).WithMany(p => p.DetalleCotizaciones)
                .HasForeignKey(d => d.CotizacionId)
                .HasConstraintName("FK__Detalle_C__Cotiz");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleCotizaciones)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Detalle_C__Produ");
        });

        modelBuilder.Entity<DetalleDevolucione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalle___3214EC0737C9A04E");

            entity.HasOne(d => d.Devolucion).WithMany(p => p.DetalleDevoluciones).HasConstraintName("FK__Detalle_D__Devol__208CD6FA");

            entity.HasOne(d => d.Usuario).WithMany(p => p.DetalleDevoluciones).HasConstraintName("FK__Detalle_D__Usuar__22751F6C");

            entity.HasOne(d => d.VentaPedido).WithMany(p => p.DetalleDevoluciones).HasConstraintName("FK__Detalle_D__Venta__2180FB33");
        });

        modelBuilder.Entity<DetalleVentaPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalle___3214EC073A97D16E");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleVentaPedidos).HasConstraintName("FK__Detalle_V__Produ__08B54D69");

            entity.HasOne(d => d.VentaPedido).WithMany(p => p.DetalleVentaPedidos).HasConstraintName("FK__Detalle_V__Venta__07C12930");
        });

        modelBuilder.Entity<Devolucione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devoluci__3214EC074BBF49D5");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estados__3214EC07AE4F961E");
        });

        modelBuilder.Entity<Imagene>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__Imagenes__B42D8F2A5DFCE52B");

            entity.HasOne(d => d.Producto).WithMany(p => p.Imagenes).HasConstraintName("FK__Imagenes__Produc__7F2BE32F");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permisos__3214EC0717827BAC");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07320121CE");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos).HasConstraintName("FK__Productos__Categ__7D439ABD");

            
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proveedo__3214EC0773176097");

            // CAMBIADO: Ahora el valor por defecto es true (activo)
            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07727368DD");
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles_Pe__3214EC07223D2819");

            entity.HasOne(d => d.Permiso).WithMany(p => p.RolesPermisos).HasConstraintName("FK__Roles_Per__Permi__71D1E811");

            entity.HasOne(d => d.Rol).WithMany(p => p.RolesPermisos).HasConstraintName("FK__Roles_Per__RolId__70DDC3D8");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC0728542FB5");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios).HasConstraintName("FK__Usuarios__RolId__74AE54BC");
        });

        modelBuilder.Entity<VentaPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Venta_Pe__3214EC073FC94792");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Estado).WithMany(p => p.VentaPedidos).HasConstraintName("FK__Venta_Ped__Estad__04E4BC85");

            entity.HasOne(d => d.Usuario).WithMany(p => p.VentaPedidos).HasConstraintName("FK__Venta_Ped__Usuar__03F0984C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}