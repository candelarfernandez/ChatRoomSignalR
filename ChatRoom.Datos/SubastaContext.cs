using System;
using System.Collections.Generic;
using ChatRoom.Datos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Datos;

public partial class SubastaContext : DbContext
{
    public SubastaContext()
    {
    }

    public SubastaContext(DbContextOptions<SubastaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<Ofertum> Oferta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Subasta;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3213E83FF347DE52");

            entity.ToTable("Notificacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mensaje");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__idVen__59063A47");
        });

        modelBuilder.Entity<Ofertum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Oferta__3213E83F1D1D8CFF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdComprador).HasColumnName("idComprador");
            entity.Property(e => e.IdSala).HasColumnName("idSala");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.IdCompradorNavigation).WithMany(p => p.Oferta)
                .HasForeignKey(d => d.IdComprador)
                .HasConstraintName("FK__Oferta__idCompra__5535A963");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Oferta)
                .HasForeignKey(d => d.IdSala)
                .HasConstraintName("FK__Oferta__idSala__5629CD9C");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3213E83FE0211E48");

            entity.ToTable("Producto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioFinal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioFinal");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sala__3213E83F37C04928");

            entity.ToTable("Sala");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activa).HasColumnName("activa");
            entity.Property(e => e.FotoProductoNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fotoProductoNombre");
            entity.Property(e => e.HoraFinalizacion)
                .HasColumnType("datetime")
                .HasColumnName("horaFinalizacion");
            entity.Property(e => e.HoraUltimaOferta)
                .HasColumnType("datetime")
                .HasColumnName("horaUltimaOferta");
            entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TiempoRestante).HasColumnName("tiempoRestante");
            entity.Property(e => e.UsuariosConectados)
                .HasColumnType("text")
                .HasColumnName("usuariosConectados");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Salas)
                .HasForeignKey(d => d.IdVendedor)
                .HasConstraintName("FK__Sala__idVendedor__52593CB8");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F4EFFA6FD");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DineroDisponible)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("dineroDisponible");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Venta__3213E83F603E29AC");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdComprador).HasColumnName("idComprador");
            entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCompradorNavigation).WithMany(p => p.VentumIdCompradorNavigations)
                .HasForeignKey(d => d.IdComprador)
                .HasConstraintName("FK__Venta__idComprad__4F7CD00D");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Venta__IdProduct__4D94879B");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.VentumIdVendedorNavigations)
                .HasForeignKey(d => d.IdVendedor)
                .HasConstraintName("FK__Venta__idVendedo__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
